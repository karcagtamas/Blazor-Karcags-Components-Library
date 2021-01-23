using System.Collections.Generic;
using System.Threading.Tasks;

namespace Karcags.Blazor.Common.Services
{
    public interface ICommonService<TModel, TElement>
    {
        Task<TElement> Get(int id);
        Task<List<TElement>> GetList();
        Task<bool> Remove(int id);
        Task<bool> Update(int id, TModel model);
        Task<bool> Create(TModel model);
    }
}