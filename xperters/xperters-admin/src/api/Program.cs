using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using ServiceStack.Logging;
using ServiceStack.Logging.Serilog;

namespace Xperters.Admin.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				var host = BuildWebHost(args);
				host.Run();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Xperters Host terminated unexpectedly");
			}
			finally
			{
				Log.Logger.Information("Xperters Shutting down");
				Log.CloseAndFlush();
			}
		}

		private static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseSerilog()
                .UseStartup<Startup>()
                .ConfigureLogging(
                    (context, loggingBuilder) =>
                    {
                        Serilog.Debugging.SelfLog.Enable(Console.Error); // this outputs internal Serilog errors to the console in case something breaks with one of the Serilog extensions or the framework itself

                        var logger = new LoggerConfiguration()
                            .Enrich.FromLogContext() // this adds more information to the output of the log, like when receiving http requests, it will provide information about the request
                            .Enrich.WithDemystifiedStackTraces() // this will change the stack trace of an exception into a more readable form if it involves async
                            .MinimumLevel.Debug() // this give the minimum level to log, in production the level would be higher
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                            .MinimumLevel.Override("System", LogEventLevel.Warning)
                            .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} {Level} {Properties} {Message}{NewLine}{Exception}") // one of the logger pipeline elements for writing out the log message
                            .WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(), "logs", "logs.txt")
                                ,outputTemplate: "{Timestamp:HH:mm:ss} [{EventType:x8} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                                , rollingInterval: RollingInterval.Day
                                , flushToDiskInterval: TimeSpan.FromMinutes(1)
                                , fileSizeLimitBytes: 1_000_000
                                , rollOnFileSizeLimit: true
                                , shared: true)
                            .WriteTo.ApplicationInsights(context.Configuration["ApplicationInsights:InstrumentationKey"], TelemetryConverter.Events)
                            .CreateLogger();

                        loggingBuilder.AddSerilog(logger); // this adds the serilog provider from the start
                    })
                    .UseSetting(WebHostDefaults.ApplicationKey,
                    typeof(Program).GetTypeInfo().Assembly.FullName) // beware of this
				.Build();
	}
}