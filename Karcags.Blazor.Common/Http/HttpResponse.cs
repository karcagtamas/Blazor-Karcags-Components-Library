namespace Karcags.Blazor.Common.Http
{
    public class HttpResponse<T>
    {
        public T Content { get; set; }
        public bool IsSuccess { get; set; }
    }
}