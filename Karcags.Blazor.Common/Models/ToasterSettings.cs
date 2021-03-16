using Karcags.Blazor.Common.Enums;

namespace Karcags.Blazor.Common.Models
{
    /// <summary>
    /// Toaster settings
    /// </summary>
    public class ToasterSettings
    {
        public string Title { get; set; }
        public string Caption { get; set; }
        public ToasterType Type { get; set; }
        public bool IsNeeded { get; set; }


        public ToasterSettings()
        {
            this.IsNeeded = false;
        }

        public ToasterSettings(string caption)
        {
            this.Caption = caption;
            this.IsNeeded = true;
        }
    }
}