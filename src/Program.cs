using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Resume_Blazor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop; // Add this using directive
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace Resume_Blazor
{
    public class Program
    {
         public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            
            builder.Services.AddSingleton(builder.Configuration.GetSection("FirebaseConfig").Get<FirebaseConfig>());

            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddScoped<SwapiService>();
            // Wait for JavaScript interop to be ready
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredToast();

            // Get Firebase configuration from appsettings.json
            //var firebaseConfig = builder.Configuration.GetSection("FirebaseConfig").Get<FirebaseConfig>();

            var host = builder.Build();



                       // After the app is built and running, get the IJSRuntime
             var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();
            var firebaseConfig = host.Services.GetRequiredService<FirebaseConfig>(); // Get FirebaseConfig from services

            // Log before calling the JS function
            Console.WriteLine("Calling setFirebaseConfig JavaScript function...");

            await jsRuntime.InvokeVoidAsync("setFirebaseConfig", firebaseConfig);

            // Log after calling the JS function
            Console.WriteLine("setFirebaseConfig JavaScript function called.");


            await host.RunAsync(); // Run the host
       
        }
    }

     public class FirebaseConfig
    {
        public string ApiKey { get; set; }
        public string AuthDomain { get; set; }
        public string ProjectId { get; set; }
        public string StorageBucket { get; set; }
        public string MessagingSenderId { get; set; }
        public string AppId { get; set; }
        public string MeasurementId { get; set; }
    }

}
