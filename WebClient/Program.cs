using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOidcAuthentication(options =>
            {
                options.ProviderOptions.Authority = "https://localhost:44313";
                options.ProviderOptions.ClientId = "BlazorClient";
                options.ProviderOptions.ResponseType = "code";
                options.ProviderOptions.DefaultScopes.Add("API");
                options.AuthenticationPaths.LogOutCallbackPath = "/";
                options.AuthenticationPaths.LogOutPath = "https://localhost:44313/logout";
                options.AuthenticationPaths.RemoteProfilePath = "https://localhost:44313/profile";
                options.AuthenticationPaths.RemoteRegisterPath = "https://localhost:44313/login";
            });

            await builder.Build().RunAsync();
        }
    }
}
