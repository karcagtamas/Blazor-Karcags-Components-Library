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
        private const string NA = "N/A";
        private readonly NavigationManager _navigationManager;
        private readonly IMatToaster _toaster;

        public HelperService(NavigationManager navigationManager, IMatToaster toaster)
        {
            this._navigationManager = navigationManager;
            this._toaster = toaster;
        }

        public void Navigate(string path)
        {
            this._navigationManager.NavigateTo(path);
        }

        public JsonSerializerOptions GetSerializerOptions()
        {
            return new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task AddToaster(HttpResponseMessage response, string caption)
        {
            if (response.IsSuccessStatusCode)
            {
                this._toaster.Add("Event successfully accomplished", MatToastType.Success, caption);
            }
            else
            {
                using (var sr = await response.Content.ReadAsStreamAsync())
                {
                    var e = await System.Text.Json.JsonSerializer.DeserializeAsync<ErrorResponse>(sr, this.GetSerializerOptions());
                    this._toaster.Add(e.Message, MatToastType.Danger, caption);
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