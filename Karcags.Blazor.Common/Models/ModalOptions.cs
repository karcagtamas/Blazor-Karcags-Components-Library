namespace Karcags.Blazor.Common.Models
{
    public class ModalOptions
    {
        public string Position { get; set; }
        public string Style { get; set; }
        public bool? DisableBackgroundCancel { get; set; }
        public bool? HideButton { get; set; }
        public bool? HideCloseButton { get; set; }
        public bool? HideHeader { get; set; }
        public ModalButtonOptions ButtonOptions { get; set; }

        public ModalOptions()
        {
            this.ButtonOptions = new ModalButtonOptions();
        }

        public ModalOptions(ModalButtonOptions options)
        {
            this.ButtonOptions = options == null ? new ModalButtonOptions() : options;
        }

    }

    public enum CancelButton
    {
        Cancel,
        Close
    }

    public enum ConfirmButton
    {
        Confirm,
        Save,
        Ok
    }
}