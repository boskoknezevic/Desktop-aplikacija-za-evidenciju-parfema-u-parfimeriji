using BazaPodataka;
using Domen.Funkcionalnosti;
using Domen.Interfejsi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ParfemUI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            var host = CreateHostBuilder();
            ApplicationConfiguration.Initialize();
            Application.Run(host.Services.GetRequiredService<ProdajaParfema>());
            await host.RunAsync();
        }

        private static IHost CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices(servisi =>
                {
                    servisi.AddTransient<ParfemService>();
                    servisi.AddScoped<IProdajaParfemaDBContext, ProdajaParfemaDBContext>();
                    servisi.AddScoped<IServiceProvider, ServiceProvider>();
                    servisi.AddScoped<ProdajaParfema>();
                })
                .Build();
        }
    }
}





        