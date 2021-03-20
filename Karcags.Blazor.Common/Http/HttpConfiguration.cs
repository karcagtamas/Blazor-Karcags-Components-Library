using System;

namespace Karcags.Blazor.Common.Http
{
    public class HttpConfiguration
    {
        public bool IsTokenBearer { get; set; }
        public Func<string> TokenGetter { get; set; }
        public Action UnauthorizedAction { get; set; }
    }
}