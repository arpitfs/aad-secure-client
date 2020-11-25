using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace AuthoriseClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hosting Application");
            RunAsync().GetAwaiter().GetResult();
        }

        private static async Task RunAsync()
        {
            var configuration = AuthConfig.BuildAppConfiguration();

            IConfidentialClientApplication application;

            application = ConfidentialClientApplicationBuilder.Create(configuration.ClientId)
                .WithClientSecret(configuration.ClientSecret)
                .WithAuthority(configuration.Authority)
                .Build();

            var resourceIds = new string[] { configuration.ResourceId };
            AuthenticationResult result = null;

            try
            {
                result = await application.AcquireTokenForClient(resourceIds).ExecuteAsync();
                Console.WriteLine("Acquiring Token");
                Console.WriteLine(result.AccessToken);
            }
            catch(MsalClientException exception)
            {
                Console.WriteLine(exception.Message);
            }

            if(!string.IsNullOrEmpty(result.AccessToken))
            {
                var httpClient = new HttpClient();
                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if(defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(
                    m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));   
                }

                defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);
                var response = await httpClient.GetAsync(configuration.BaseAddress);

                if(response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(data);
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve data from API {response.StatusCode}");
                    var data = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(data);
                }
            }

        }
    }
}
