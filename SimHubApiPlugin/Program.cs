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
using System.Threading;

namespace SimHubApiPlugin
{
    public class Program
    {
        public static IDisposable Start(int port)
        {
            var host = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseUrls("http://*:" + port.ToString())
                .Build();

            host.RunAsync();

            return new ActionDisposable(() => host.StopAsync());
        }

        private class ActionDisposable : IDisposable
        {
            private readonly Action action;

            public ActionDisposable(Action action)
            {
                this.action = action;
            }

            public void Dispose()
            {
                action();
            }
        }
    }
}
