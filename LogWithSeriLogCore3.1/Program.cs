using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;

namespace LogWithSeriLogCore3._1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog((builder, logger) =>
                    {
                        if (builder.HostingEnvironment.IsDevelopment())
                        {
                            logger.WriteTo.Console().MinimumLevel.Information();
                        }
                        else if (builder.HostingEnvironment.IsProduction())
                        {
                            logger.WriteTo.MSSqlServer("Data Source=.;Initial Catalog=Logs;Trusted_Connection=True;", new SinkOptions {AutoCreateSqlTable=true,TableName="Logs" }).MinimumLevel.Error();
                        }
                    });
                });
    }
}
