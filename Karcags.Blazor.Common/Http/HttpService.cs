using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Karcags.Blazor.Common.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Karcags.Blazor.Common.Http
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IHelperService _helperService;
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpConfiguration _configuration;
        private readonly ILocalStorageService _localStorageService;
        private readonly NavigationManager _navigationManager;

        /// <summary>
        /// HTTP Service Injector
        /// </summary>
        /// <param name="httpClient">HTTP Client</param>
        /// <param name="helperService">Helper Service</param>
        /// <param name="jsRuntime">Js Runtime</param>
        /// <param name="configuration">HTTP Configuration</param>
        /// <param name="localStorageService">Local Storage Service</param>
        /// <param name="navigationManager">Navigation Manager</param>
        public HttpService(HttpClient httpClient, IHelperService helperService, IJSRuntime jsRuntime,
            HttpConfiguration configuration, ILocalStorageService localStorageService, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _helperService = helperService;
            _jsRuntime = jsRuntime;
            _configuration = configuration;
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
        }

        /// <summary>
        /// POST request
        /// </summary>
        /// <param name="settings">HTTP settings</param>
        /// <param name="body">Body of post request</param>
        /// <typeparam name="T">Type of the body</typeparam>
        /// <returns>The request was success or not</returns>
        public async Task<bool> Create<T>(HttpSettings settings, HttpBody<T> body)
        {
            return (await SendRequest<T>(settings, HttpMethod.Post, null)).IsSuccess;
        }

        /// <summary>
        /// POST request where we want string response
        /// </summary>
        /// <param name="settings">HTTP settings</param>
        /// <param name="body">Body of post request</param>
        /// <typeparam name="T">Type of the body</typeparam>
        /// <returns>Response string value</returns>
        public async Task<string> CreateString<T>(HttpSettings settings, HttpBody<T> body)
        {
            return await CreateWithResult<string, T>(settings, body);
        }

        /// <summary>
        /// DELETE request
        /// </summary>
        /// <param name="settings">HTTP settings</param>
        /// <returns>The request was success or not</returns>
        public async Task<bool> Delete(HttpSettings settings)
        {
            return (await SendRequest<object>(settings, HttpMethod.Delete, null)).IsSuccess;
        }

        /// <summary>
        /// GET request
        /// </summary>
        /// <param name="settings">HTTP settings</param>
        /// <typeparam name="T">Type of the result</typeparam>
        /// <returns>Response as T type</returns>
        public async Task<T> Get<T>(HttpSettings settings)
        {
            return (await SendRequest<T>(settings, HttpMethod.Get, null)).Content;
        }

        /// <summary>
        /// Get number
        /// </summary>
        /// <param name="settings">HTTP settings</param>
        /// <returns>Number response</returns>
        public async Task<int?> GetInt(HttpSettings settings)
        {
            return await Get<int?>(settings);
        }

        /// <summary>
        /// Get string
        /// </summary>
        /// <param name="settings">HTTP settings</param>
        /// <returns>String response</returns>
        public async Task<string> GetString(HttpSettings settings)
        {
            return await Get<string>(settings);
        }

        /// <summary>
        /// PUT request
        /// </summary>
        /// <param name="settings">HTTP settings</param>
        /// <param name="body">Body of put request</param>
        /// <typeparam name="T">Type of the body</typeparam>
        /// <returns>The request was success or not</returns>
        public async Task<bool> Update<T>(HttpSettings settings, HttpBody<T> body)
        {
            return (await SendRequest<T>(settings, HttpMethod.Put, body.GetStringContent())).IsSuccess;
        }

        public async Task<T> UpdateWithResult<T, TBody>(HttpSettings settings, HttpBody<TBody> body)
        {
            return (await SendRequest<T>(settings, HttpMethod.Put, body.GetStringContent())).Content;
        }

        public async Task<int> CreateInt<T>(HttpSettings settings, HttpBody<T> body)
        {
            return await CreateWithResult<int, T>(settings, body);
        }

        public async Task<bool> Download(HttpSettings settings)
        {
            return Download(await Get<ExportResult>(settings));
        }

        public async Task<bool> Download<T>(HttpSettings settings, T model)
        {
            var body = new HttpBody<T>(model);

            return Download(await UpdateWithResult<ExportResult, T>(settings, body));
        }

        public async Task<T> CreateWithResult<T, TBody>(HttpSettings settings, HttpBody<TBody> body)
        {
            return (await SendRequest<T>(settings, HttpMethod.Post, body.GetStringContent())).Content;
        }

        private async Task<HttpResponse<T>> SendRequest<T>(HttpSettings settings, HttpMethod method,
            HttpContent content)
        {
            CheckSettings(settings);

            var url = CreateUrl(settings);
            var request = new HttpRequestMessage(method, url);
            if (content != null)
            {
                request.Content = content;
            }

            if (_configuration.IsTokenBearer)
            {
                var token = await _localStorageService.GetItemAsync<string>(_configuration.TokenName);
                var isApiUrl = !request.RequestUri?.IsAbsoluteUri ?? false;

                if (!string.IsNullOrEmpty(token) && isApiUrl)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }

            try
            {
                using var response = await _httpClient.SendAsync(request);

                if (CheckActionWasUnauthorized(response))
                {
                    return new HttpResponse<T> {IsSuccess = false};
                }

                if (settings.ToasterSettings.IsNeeded)
                {
                    await _helperService.AddToaster(response, settings.ToasterSettings.Caption);
                }

                if (!response.IsSuccessStatusCode)
                {
                    return new HttpResponse<T> {IsSuccess = false};
                }

                try
                {
                    return new HttpResponse<T>
                        {IsSuccess = false, Content = await response.Content.ReadFromJsonAsync<T>()};
                }
                catch (Exception e)
                {
                    ConsoleSerializationError(e);
                    return new HttpResponse<T> {IsSuccess = false};
                }
            }
            catch (Exception e)
            {
                ConsoleCallError(e, url);
                return new HttpResponse<T> {IsSuccess = false};
            }
        }

        private bool Download(ExportResult result)
        {
            if (_jsRuntime is IJSUnmarshalledRuntime unmarshalledRuntime)
            {
                unmarshalledRuntime.InvokeUnmarshalled<string, string, byte[], bool>("manageDownload", result.FileName,
                    result.ContentType, result.Content);
            }

            return true;
        }

        private bool CheckActionWasUnauthorized(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.Unauthorized) return false;

            _navigationManager.NavigateTo(_configuration.UnauthorizedPath);
            return true;
        }

        /// <summary>
        /// Create URL from HTTP settings
        /// Concatenate URL, path parameters and query parameters
        /// </summary>
        /// <param name="settings">HTTP settings</param>
        /// <returns>Created URL</returns>
        private string CreateUrl(HttpSettings settings)
        {
            string url = settings.Url;

            if (settings.PathParameters.Count() > 0)
            {
                url += settings.PathParameters.ToString();
            }

            if (settings.QueryParameters.Count() > 0)
            {
                url += $"?{settings.QueryParameters}";
            }

            return url;
        }

        private void CheckSettings(HttpSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentException("Settings cannot be null");
            }
        }

        private void ConsoleSerializationError(Exception e)
        {
            Console.WriteLine("Serialization Error: ");
            Console.WriteLine(e);
        }

        private void ConsoleCallError(Exception e, string url)
        {
            Console.WriteLine($"HTTP Call Error from {url}: ");
            Console.WriteLine(e);
        }
    }
}