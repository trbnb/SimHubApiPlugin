using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Sockets;
using System.Threading;
using SimHub;

namespace SimHubApiPlugin.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddJsonFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseWebSockets();
            app.UseMvc();
        }
        
        public static void StartWebHost(
            int port,
            Action<WebHostBuilderContext, IServiceCollection> configureServices,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                Logging.Current.Info($"SimHubApiPlugin plugin listening to {port} (User friendly port)");
                new WebHostBuilder()
                    .UseKestrel(serverOptions => serverOptions.ConfigureEndpointDefaults(listenOptions => listenOptions.NoDelay = true))
                    .ConfigureServices(configureServices)
                    .UseStartup<Startup>()
                    .UseUrls($"http://*:{port}")
                    .UseWebRoot("Web")
                    .Build()
                    .RunAsync(cancellationToken);
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode != SocketError.AccessDenied)
                {
                    if (ex.SocketErrorCode != SocketError.AddressAlreadyInUse) return;
                }
                var message = $"Could not start dashboards Web server, TCP port {port} is busy or blocked, make sure no other apps are using the port, check your firewall, or try changing web server port in SimHub settings and restart SimHub";
                Logging.Current.Error(message, ex);
            }
            catch (Exception ex)
            {
                Logging.Current.Error(ex);
            }
        }
    }
}
