namespace Karcags.Blazor.Common.Http
{
    internal class ExportResult
    {
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}