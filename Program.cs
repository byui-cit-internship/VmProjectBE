using DatabaseVmProject.Services;
using Microsoft.EntityFrameworkCore;
using DatabaseVmProject.DAL;
using Microsoft.EntityFrameworkCore.Design;

namespace DatabaseVmProject
{
    public class Program
    {
        public static void Main(string[] args)
        {

            IHost host = CreateHostBuilder(args).Build();

            IServiceProvider services = host.Services.CreateScope().ServiceProvider;

            CreateDbOrMigrate(services);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void CreateDbOrMigrate(IServiceProvider services)
        {
            VmEntities context = services.GetRequiredService<VmEntities>();
            try
            {
                context.Database.Migrate();
            }
            catch (Exception e)
            {
            }
        }
    }
}
