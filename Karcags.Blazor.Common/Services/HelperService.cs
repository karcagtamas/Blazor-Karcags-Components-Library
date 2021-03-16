using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EventManager.Client.Models;
using Karcags.Blazor.Common.Enums;
using Karcags.Blazor.Common.Models;
using Microsoft.AspNetCore.Components;

namespace Karcags.Blazor.Common.Services
{
    public class HelperService : IHelperService
    {
        protected const string NA = "N/A";
        protected readonly NavigationManager NavigationManager;
        protected readonly IToasterService ToasterService;

        public HelperService(NavigationManager navigationManager, IToasterService toasterService)
        {
            this.NavigationManager = navigationManager;
            this.ToasterService = toasterService;
        }

        public void Navigate(string path)
        {
            this.NavigationManager.NavigateTo(path);
        }

        public JsonSerializerOptions GetSerializerOptions()
        {
            return new() {PropertyNameCaseInsensitive = true};
        }

        public async Task AddToaster(HttpResponseMessage response, string caption)
        {
            if (response.IsSuccessStatusCode)
            {
                ToasterService.Open(new ToasterSettings
                    {Title = "Event successfully accomplished", Caption = caption, Type = ToasterType.Success});
            }
            else
            {
                await using var sr = await response.Content.ReadAsStreamAsync();
                var e = await JsonSerializer.DeserializeAsync<ErrorResponse>(sr, this.GetSerializerOptions());
                ToasterService.Open(new ToasterSettings
                    {Title = e?.Message ?? "-", Caption = caption, Type = ToasterType.Error});
            }
        }

        public decimal MinToHour(int min)
        {
            return min / (decimal) 60;
        }

        public int CurrentYear()
        {
            return DateTime.Today.Year;
        }

        public int CurrentMonth()
        {
            return DateTime.Today.Month;
        }

        public DateTime CurrentWeek()
        {
            var date = DateTime.Today;
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }

            return date;
        }
    }
}