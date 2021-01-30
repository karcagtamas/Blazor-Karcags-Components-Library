using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EventManager.Client.Models;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace Karcags.Blazor.Common.Services
{
    public class HelperService : IHelperService
    {
        protected const string NA = "N/A";
        protected readonly NavigationManager NavigationManager;
        protected readonly IMatToaster Toaster;

        public HelperService(NavigationManager navigationManager, IMatToaster toaster)
        {
            this.NavigationManager = navigationManager;
            this.Toaster = toaster;
        }

        public void Navigate(string path)
        {
            this.NavigationManager.NavigateTo(path);
        }

        public JsonSerializerOptions GetSerializerOptions()
        {
            return new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task AddToaster(HttpResponseMessage response, string caption)
        {
            if (response.IsSuccessStatusCode)
            {
                this.Toaster.Add("Event successfully accomplished", MatToastType.Success, caption);
            }
            else
            {
                using (var sr = await response.Content.ReadAsStreamAsync())
                {
                    var e = await System.Text.Json.JsonSerializer.DeserializeAsync<ErrorResponse>(sr, this.GetSerializerOptions());
                    this.Toaster.Add(e.Message, MatToastType.Danger, caption);
                }
            }
        }

        public decimal MinToHour(int min)
        {
            return min / (decimal)60;
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