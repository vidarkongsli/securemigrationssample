using System.CommandLine;
using System.CommandLine.Invocation;
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
        public static async Task<int> Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var rootCommand = new RootCommand{
                new Option<bool>("--migrate", "Whether to run database migrations"),
                new Option<bool>("--thenExit", "Whether to exit after migrations (instead of starting web app)")
            };

            rootCommand.Handler = CommandHandler.Create<bool, bool>(async (migrate, thenExit) =>
            {
                if (migrate)
                {
                    using (var scope = host.Services.CreateScope())
                    {
                        var sampleContext = scope.ServiceProvider.GetService<SampleContext>();
                        WriteLine($"Running migrations on db {sampleContext.Database.GetDbConnection().ConnectionString}");
                        await sampleContext.Database.MigrateAsync();
                        WriteLine("Done running migrations");
                    }
                }
                else {
                    WriteLine("Skipping migrations");
                }
                if (!thenExit)
                {
                    WriteLine("Starting web app.");
                    await host.RunAsync();
                }
                else {
                    WriteLine("Exit without starting web app.");
                }
            });
            return await rootCommand.InvokeAsync(args);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
