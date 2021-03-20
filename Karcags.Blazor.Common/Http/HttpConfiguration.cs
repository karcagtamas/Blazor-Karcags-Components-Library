using System;

namespace Karcags.Blazor.Common.Http
{
    public class HttpConfiguration
    {
        public bool IsTokenBearer { get; set; }
        public string TokenName { get; set; }
        public string UnauthorizedPath { get; set; }
    }
}