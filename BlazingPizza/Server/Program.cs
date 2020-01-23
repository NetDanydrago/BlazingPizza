using System.Linq;
using BlazingPizza.Server.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BlazingPizza.Server
{
    public class Program
    {

        /// <summary>
        /// Obtener Contextode datos a traves del servicion de Injeccion de  Dependecias y ejecutar el metodo SeedData.Initialize cuando
        /// lata tabla specials no tenga registros
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var Host = BuildWebHost(args);
            var ScopeFactory = Host.Services.GetRequiredService<IServiceScopeFactory>();
            using (var Scope = ScopeFactory.CreateScope())
            {
                var Context = Scope.ServiceProvider
                    .GetRequiredService<PizzaStoreContext>();
                if(Context.Specials.Count() == 0)
                {
                    SeedData.Initialize(Context);
                }
            }
                Host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .Build())
                .UseStartup<Startup>()
                .Build();
    }
}
