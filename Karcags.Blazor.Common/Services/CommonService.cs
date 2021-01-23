using System.Collections.Generic;
using System.Threading.Tasks;
using Karcags.Blazor.Common.Http;

namespace Karcags.Blazor.Common.Services
{
    public class CommonService<TModel, TElement> : ICommonService<TModel, TElement>
    {
        private readonly IHttpService _httpService;
        private string Url { get; init; }
        private string Entity { get; init; }

        public CommonService(string url, string entity, IHttpService httpService)
        {
            _httpService = httpService;
            Url = url;
            Entity = entity;
        }

        public async Task<TElement> Get(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add(id, -1);

            var settings = new HttpSettings($"{Url}/{Entity}", null, pathParams);

            return await _httpService.Get<TElement>(settings);
        }

        public async Task<List<TElement>> GetList()
        {
            var settings = new HttpSettings($"{Url}/{Entity}");

            return await _httpService.Get<List<TElement>>(settings);
        }

        public async Task<bool> Remove(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add(id, -1);

            var settings = new HttpSettings($"{Url}/{Entity}", null, pathParams);

            return await _httpService.Delete(settings);
        }

        public async Task<bool> Update(int id, TModel model)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add(id, -1);

            var settings = new HttpSettings($"{Url}/{Entity}", null, pathParams);

            var body = new HttpBody<TModel>(model);

            return await _httpService.Update(settings, body);
        }

        public async Task<bool> Create(TModel model)
        {
            var settings = new HttpSettings($"{Url}/{Entity}");

            var body = new HttpBody<TModel>(model);

            return await _httpService.Create(settings, body);
        }
    }
}