using System.Collections.Generic;
using System.Threading.Tasks;
using Karcags.Blazor.Common.Http;

namespace Karcags.Blazor.Common.Services
{
    public class CommonService<TModel, TElement> : ICommonService<TModel, TElement>
    {
        protected readonly IHttpService HttpService;
        protected string Url { get; init; }
        protected string Entity { get; init; }

        public CommonService(string url, string entity, IHttpService httpService)
        {
            HttpService = httpService;
            Url = url;
            Entity = entity;
        }

        public async Task<TElement> Get(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add(id, -1);

            var settings = new HttpSettings($"{Url}/{Entity}", null, pathParams);

            return await HttpService.Get<TElement>(settings);
        }

        public async Task<List<TElement>> GetList()
        {
            var settings = new HttpSettings($"{Url}/{Entity}");

            return await HttpService.Get<List<TElement>>(settings);
        }

        public async Task<bool> Remove(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add(id, -1);

            var settings = new HttpSettings($"{Url}/{Entity}", null, pathParams);

            return await HttpService.Delete(settings);
        }

        public async Task<bool> Update(int id, TModel model)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add(id, -1);

            var settings = new HttpSettings($"{Url}/{Entity}", null, pathParams);

            var body = new HttpBody<TModel>(model);

            return await HttpService.Update(settings, body);
        }

        public async Task<bool> Create(TModel model)
        {
            var settings = new HttpSettings($"{Url}/{Entity}");

            var body = new HttpBody<TModel>(model);

            return await HttpService.Create(settings, body);
        }
    }
}