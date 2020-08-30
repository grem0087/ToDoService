using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ToDo.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.AddSerilog();
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddFilter("Microsoft", LogLevel.Warning)
                            .AddFilter("System", LogLevel.Information)
                            .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.ListenLocalhost(5001,
                                   listenOptions =>
                                   {
                                       listenOptions.UseHttps();
                                   });

                    });
                    webBuilder.UseStartup<Startup>();;
                });
    }
}