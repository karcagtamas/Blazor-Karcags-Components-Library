using System;
using System.Net.Http;
using Karcags.Blazor.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.JSInterop;

namespace Karcags.Blazor.Common.Http
{
    public static class HttpExtension
    {
        public static IServiceCollection AddHttpService(this IServiceCollection serviceCollection,
            Action<HttpConfiguration> configuration)
        {
            var conf = new HttpConfiguration();
            configuration(conf);
            serviceCollection.TryAddScoped<IHttpService>((Func<IServiceProvider, IHttpService>) (builder =>
                (IHttpService) new HttpService(builder.GetService<HttpClient>(), builder.GetService<IHelperService>(),
                    builder.GetService<IJSRuntime>(), conf)));
            return serviceCollection;
        }
    }
}