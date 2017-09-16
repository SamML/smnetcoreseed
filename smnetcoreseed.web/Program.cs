using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace smnetcoreseed.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
               .AddEnvironmentVariables(prefix: "ASPNETCORE_")
               .Build();

            BuildWebHost(args, config).Run();
        }

        public static IWebHost BuildWebHost(string[] args, IConfigurationRoot config)
        {
            return WebHost.CreateDefaultBuilder(args)
                  .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
.UseStartup<Startup>()
 .UseApplicationInsights()
.Build();
        }
    }
}