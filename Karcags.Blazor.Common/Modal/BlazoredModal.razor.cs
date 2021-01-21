using System;
using Karcags.Blazor.Common.Models;
using Karcags.Blazor.Common.Services;
using Microsoft.AspNetCore.Components;

namespace Karcags.Blazor.Common.Modal
{
    public partial class BlazoredModal : IDisposable
    {
        private const string DefaultStyle = "blazored-modal";
        private const string DefaultPosition = "blazored-modal-center";

        [Inject] private IModalService ModalService { get; set; }

        [Parameter] public bool HideHeader { get; set; }

        [Parameter] public bool HideCloseButton { get; set; }

        [Parameter] public bool DisableBackgroundCancel { get; set; }

        [Parameter] public string Position { get; set; }

        [Parameter] public string Style { get; set; }

        private bool ComponentDisableBackgroundCancel { get; set; }
        private bool ComponentHideHeader { get; set; }
        private bool ComponentHideCloseButton { get; set; }
        private string ComponentPosition { get; set; }
        private string ComponentStyle { get; set; }
        private bool IsVisible { get; set; }
        private string Title { get; set; }
        private bool ShowCancelButton { get; set; }
        private bool ShowConfirmButton { get; set; }
        private string CancelButtonText { get; set; }
        private string ConfirmButtonText { get; set; }
        private RenderFragment Content { get; set; }
        private ModalParameters Parameters { get; set; }

        protected override void OnInitialized()
        {
            ((ModalService)this.ModalService).OnShow += this.ShowModal;
            ((ModalService)this.ModalService).CloseModal += this.CloseModal;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private async void ShowModal(string title, RenderFragment content, ModalParameters parameters,
            ModalOptions options)
        {
            this.Title = title;
            this.Content = content;
            this.Parameters = parameters;
            this.IsVisible = true;

            if (options != null)
            {
                this.SetButtonSettings(options);
            }

            await this.InvokeAsync(this.StateHasChanged);
        }

        private async void CloseModal()
        {
            this.Title = "";
            this.Content = null;
            this.ComponentStyle = "";
            this.IsVisible = false;
            await this.InvokeAsync(this.StateHasChanged);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((ModalService)this.ModalService).OnShow -= this.ShowModal;
                ((ModalService)this.ModalService).CloseModal -= this.CloseModal;
            }
        }

        private void HandleBackgroundClick(bool value)
        {
            if (!value)
            {
                if (this.ComponentDisableBackgroundCancel)
                {
                    return;
                }

                this.ModalService.Cancel();
            }
        }

        private void SetModalOptions(ModalOptions options)
        {
            this.ComponentHideHeader = this.HideHeader;
            if (options.HideHeader.HasValue)
            {
                this.ComponentHideHeader = options.HideHeader.Value;
            }

            this.ComponentHideCloseButton = this.HideCloseButton;
            if (options.HideCloseButton.HasValue)
            {
                this.ComponentHideCloseButton = options.HideCloseButton.Value;
            }

            this.ComponentDisableBackgroundCancel = this.DisableBackgroundCancel;
            if (options.DisableBackgroundCancel.HasValue)
            {
                this.ComponentDisableBackgroundCancel = options.DisableBackgroundCancel.Value;
            }

            this.ComponentPosition = string.IsNullOrWhiteSpace(options.Position) ? this.Position : options.Position;
            if (string.IsNullOrWhiteSpace(this.ComponentPosition))
            {
                this.ComponentPosition = DefaultPosition;
            }

            this.ComponentStyle = string.IsNullOrWhiteSpace(options.Style) ? this.Style : options.Style;
            if (string.IsNullOrWhiteSpace(this.ComponentStyle))
            {
                this.ComponentStyle = DefaultStyle;
            }
        }

        public void SetTitle(string title)
        {
            this.Title = title;
            this.StateHasChanged();
        }

        public void SetButtonSettings(ModalOptions options)
        {
            this.ShowCancelButton = options.ButtonOptions.ShowCancelButton;
            this.ShowConfirmButton = options.ButtonOptions.ShowConfirmButton;
            this.CancelButtonText = this.GetNameOfButtonType(options.ButtonOptions.CancelButtonType);
            this.ConfirmButtonText = this.GetNameOfButtonType(options.ButtonOptions.ConfirmButtonType);
        }

        public string GetNameOfButtonType(ConfirmButton type)
        {
            return type.ToString();
        }

        public string GetNameOfButtonType(CancelButton type)
        {
            return type.ToString();
        }
    }
}