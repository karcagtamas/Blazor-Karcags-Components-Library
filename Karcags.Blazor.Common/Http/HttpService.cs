using System;
using System.Net.Http;
using System.Threading.Tasks;
using Karcags.Blazor.Common.Services;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Karcags.Blazor.Common.Http
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IHelperService _helperService;
        private readonly IJSRuntime _jsRuntime;

        /// <summary>
        /// HTTP Service Injector
        /// </summary>
        /// <param name="httpClient">HTTP Client</param>
        /// <param name="helperService">Helper Service</param>
        public HttpService(HttpClient httpClient, IHelperService helperService, IJSRuntime jsRuntime)
        {
            this._httpClient = httpClient;
            this._helperService = helperService;
            this._jsRuntime = jsRuntime;
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
            this.CheckSettings(settings);

            string url = this.CreateUrl(settings);

            HttpResponseMessage response;

            try
            {

                response = await this._httpClient.PostAsync(url, body.GetStringContent());
            }
            catch (Exception e)
            {
                this.ConsoleCallError(e, url);
                return false;
            }

            // Optional toast
            if (settings.ToasterSettings.IsNeeded)
            {
                await this._helperService.AddToaster(response, settings.ToasterSettings.Caption);
            }

            return response.IsSuccessStatusCode;
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
            this.CheckSettings(settings);

            string url = this.CreateUrl(settings);

            HttpResponseMessage response;

            try
            {
                response = await this._httpClient.PostAsync(url, body.GetStringContent());
            }
            catch (Exception e)
            {
                this.ConsoleCallError(e, url);
                return default;
            }


            // Optional toast
            if (settings.ToasterSettings.IsNeeded)
            {
                await this._helperService.AddToaster(response, settings.ToasterSettings.Caption);
            }

            // De-serialize JSON
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    this.ConsoleSerializationError(e);
                    return default;
                }
            }

            return default;
        }

        /// <summary>
        /// DELETE request
        /// </summary>
        /// <param name="settings">HTTP settings</param>
        /// <returns>The request was success or not</returns>
        public async Task<bool> Delete(HttpSettings settings)
        {
            this.CheckSettings(settings);

            string url = this.CreateUrl(settings);

            HttpResponseMessage response;

            try
            {
                response = await this._httpClient.DeleteAsync(url);
            }
            catch (Exception e)
            {
                this.ConsoleCallError(e, url);
                return false;
            }

            // Optional toast
            if (settings.ToasterSettings.IsNeeded)
            {
                await this._helperService.AddToaster(response, settings.ToasterSettings.Caption);
            }

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// GET request
        /// </summary>
        /// <param name="settings">HTTP settings</param>
        /// <typeparam name="T">Type of the result</typeparam>
        /// <returns>Response as T type</returns>
        public async Task<T> Get<T>(HttpSettings settings)
        {
            this.CheckSettings(settings);

            string url = this.CreateUrl(settings);

            HttpResponseMessage response;

            try
            {
                response = await this._httpClient.GetAsync(url);
            }
            catch (Exception e)
            {
                this.ConsoleCallError(e, url);
                return default;
            }

            // De-serialize JSON
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    using (var sr = await response.Content.ReadAsStreamAsync())
                    {
                        return await JsonSerializer.DeserializeAsync<T>(sr, this._helperService.GetSerializerOptions());
                    }
                }
                catch (Exception e)
                {
                    this.ConsoleSerializationError(e);
                    return default;
                }
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// Get number
        /// </summary>
        /// <param name="settings">HTTP settings</param>
        /// <returns>Number response</returns>
        public async Task<int?> GetInt(HttpSettings settings)
        {
            this.CheckSettings(settings);

            string url = this.CreateUrl(settings);

            HttpResponseMessage response;

            try
            {
                response = await this._httpClient.GetAsync(url);
            }
            catch (Exception e)
            {
                this.ConsoleCallError(e, url);
                return default;
            }

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    int count = -1;
                    int.TryParse(await response.Content.ReadAsStringAsync(), out count);
                    return count == -1 ? null : (int?)count;
                }
                catch (Exception e)
                {
                    this.ConsoleSerializationError(e);
                    return default;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get string
        /// </summary>
        /// <param name="settings">HTTP settings</param>
        /// <returns>String response</returns>
        public async Task<string> GetString(HttpSettings settings)
        {
            this.CheckSettings(settings);

            string url = this.CreateUrl(settings);

            HttpResponseMessage response;

            try
            {
                response = await this._httpClient.GetAsync(url);
            }
            catch (Exception e)
            {
                this.ConsoleCallError(e, url);
                return default;
            }


            // De-serialize JSON
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    this.ConsoleSerializationError(e);
                    return default;
                }
            }
            else
            {
                return default;
            }
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
            this.CheckSettings(settings);

            string url = this.CreateUrl(settings);

            HttpResponseMessage response;

            try
            {
                response = await this._httpClient.PutAsync(url, body.GetStringContent());
            }
            catch (Exception e)
            {
                this.ConsoleCallError(e, url);
                return false;
            }

            // Optional toast
            if (settings.ToasterSettings.IsNeeded)
            {
                await this._helperService.AddToaster(response, settings.ToasterSettings.Caption);
            }

            return response.IsSuccessStatusCode;
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
                url += $"?{settings.QueryParameters.ToString()}";
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

        public async Task<T> UpdateWithResult<T, V>(HttpSettings settings, HttpBody<V> body)
        {
            this.CheckSettings(settings);

            string url = this.CreateUrl(settings);

            HttpResponseMessage response;

            try
            {
                response = await this._httpClient.PutAsync(url, body.GetStringContent());
            }
            catch (Exception e)
            {
                this.ConsoleCallError(e, url);
                return default;
            }

            // Optional toast
            if (settings.ToasterSettings.IsNeeded)
            {
                await this._helperService.AddToaster(response, settings.ToasterSettings.Caption);
            }

            // De-serialize JSON
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    using (var sr = await response.Content.ReadAsStreamAsync())
                    {
                        return await JsonSerializer.DeserializeAsync<T>(sr, this._helperService.GetSerializerOptions());
                    }
                }
                catch (Exception e)
                {
                    this.ConsoleSerializationError(e);
                    return default;
                }
            }
            else
            {
                return default;
            }
        }

        public async Task<int> CreateInt<T>(HttpSettings settings, HttpBody<T> body)
        {
            return int.Parse(await this.CreateString(settings, body));
        }

        public async Task<bool> Download(HttpSettings settings)
        {
            return this.Download(await this.Get<ExportResult>(settings));
        }

        public async Task<bool> Download<T>(HttpSettings settings, T model)
        {
            var body = new HttpBody<T>(model);

            return this.Download(await this.UpdateWithResult<ExportResult, T>(settings, body));
        }

        private bool Download(ExportResult result)
        {
            if (this._jsRuntime is IJSUnmarshalledRuntime unmarshalledRuntime)
            {
                unmarshalledRuntime.InvokeUnmarshalled<string, string, byte[], bool>("manageDownload", result.FileName, result.ContentType, result.Content);
            }

            return true;
        }
    }
}