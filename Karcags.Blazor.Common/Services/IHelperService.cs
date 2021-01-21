using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Karcags.Blazor.Common.Services
{
    public interface IHelperService
    {
        void Navigate(string path);
        JsonSerializerOptions GetSerializerOptions();
        Task AddToaster(HttpResponseMessage response, string caption);
        decimal MinToHour(int min);
        int CurrentYear();
        int CurrentMonth();
        DateTime CurrentWeek();
    }
}