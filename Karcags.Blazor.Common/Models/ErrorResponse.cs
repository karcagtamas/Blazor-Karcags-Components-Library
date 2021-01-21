using System;

namespace EventManager.Client.Models
{

    /// <summary>
    /// Error Response
    /// </summary>
    public class ErrorResponse
    {
        public string Message { get; set; }
        public ErrorResponse Inner { get; set; }
        public string StackTrace { get; set; }

        // For deserialization
        public ErrorResponse()
        {

        }

        public ErrorResponse(Exception e)
        {
            this.Message = e.Message;

            // Inner exception
            if (e.InnerException != null)
            {
                this.Inner = new ErrorResponse(e.InnerException);
            }

            // Stack trace
            if (e.StackTrace != null)
            {
                this.StackTrace = e.StackTrace;
            }
        }
    }
}