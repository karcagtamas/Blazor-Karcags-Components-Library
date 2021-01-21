using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Karcags.Blazor.Common.Http
{

    /// <summary>
    /// HTTP body
    /// </summary>
    /// <typeparam name="T">Type of the content</typeparam>
    public class HttpBody<T>
    {
        private T Body { get; set; }

        /// <summary>
        /// Create body
        /// </summary>
        /// <param name="body">Content</param>
        public HttpBody(T body)
        {
            this.Body = body;
        }

        /// <summary>
        /// Create string content from the body
        /// </summary>
        /// <returns>String content</returns>
        public StringContent GetStringContent()
        {
            return this.Body == null ? new StringContent("") : this.CreateContent(this.Body);
        }

        /// <summary>
        /// Create String Content
        /// </summary>
        /// <param name="obj">Object for creation</param>
        /// <returns>String content</returns>
        private StringContent CreateContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
    }
}