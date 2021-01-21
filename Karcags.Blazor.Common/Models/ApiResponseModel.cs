namespace Karcags.Blazor.Common.Models
{
    public class ApiResponseModel<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string StatusCode { get; set; }
        public T Content { get; set; }
    }
}