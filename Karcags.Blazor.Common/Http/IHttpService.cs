using System.Threading.Tasks;

namespace Karcags.Blazor.Common.Http
{
    public interface IHttpService
    {
        Task<T> Get<T>(HttpSettings settings);
        Task<string> GetString(HttpSettings settings);
        Task<int?> GetInt(HttpSettings settings);
        Task<bool> Delete(HttpSettings settings);
        Task<bool> Update<T>(HttpSettings settings, HttpBody<T> body);
        Task<bool> Create<T>(HttpSettings settings, HttpBody<T> body);
        Task<string> CreateString<T>(HttpSettings settings, HttpBody<T> body);
        Task<T> UpdateWithResult<T, TBody>(HttpSettings settings, HttpBody<TBody> body);
        Task<int> CreateInt<T>(HttpSettings settings, HttpBody<T> body);
        Task<bool> Download(HttpSettings settings);
        Task<bool> Download<T>(HttpSettings settings, T model);
        Task<T> CreateWithResult<T, TBody>(HttpSettings settings, HttpBody<TBody> body);
    }
}