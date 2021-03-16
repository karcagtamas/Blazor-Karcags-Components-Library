using Karcags.Blazor.Common.Models;

namespace Karcags.Blazor.Common.Services
{
    public interface IToasterService
    {
        void Open(ToasterSettings settings);
    }
}