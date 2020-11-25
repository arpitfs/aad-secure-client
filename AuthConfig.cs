using System.IO;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace AuthoriseClient
{
    public class AuthConfig
    {
        public string Instance { get; set; }        
        public string TenantId { get; set; }    
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string BaseAddress { get; set; }
        public string ResourceId { get; set; }
        public string Authority
        { 
            get
            {
                return string.Format(CultureInfo.InvariantCulture, Instance, TenantId);
            } 
        }

        public static AuthConfig BuildAppConfiguration()
        {
            IConfiguration configuration;

            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            return configuration.Get<AuthConfig>();
        }
    }
}