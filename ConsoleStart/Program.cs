using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;

namespace ConsoleStart
{
    class Program
    {

        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build()
                .Run();
        }

        static void Main2(string[] args)
        {
            new WebHostBuilder()
                //.UseStartup<Sim>()
                .Build()
                .RunAsync();

            Console.ReadLine();

            /**
             * new WebHostBuilder()
             *      .UseKestrel((serverOptions => serverOptions.ConfigureEndpointDefaults((Action<ListenOptions>) (listenOptions => listenOptions.NoDelay = true))))
             *      .UseStartup<KestrelStartup>()
             *      .UseUrls("http://*:" + port.ToString())
             *      .UseWebRoot("Web")
             *      .Build()
             *      .Run();
             */
        }
    }
}
