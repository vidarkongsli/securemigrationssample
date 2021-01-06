using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Console;

namespace SecureMigrationsSample.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            if (args.Contains("--migrate"))
            using (var scope = host.Services.CreateScope())
            {
                var sampleContext = scope.ServiceProvider.GetService<SampleContext>();
                WriteLine($"Running migrations on db {sampleContext.Database.GetDbConnection().ConnectionString}");
                await sampleContext.Database.MigrateAsync();
                WriteLine("Done running migrations");
            }
            
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
