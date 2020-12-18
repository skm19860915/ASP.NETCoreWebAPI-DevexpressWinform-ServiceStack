using System;
using System.Configuration;
using System.Windows.Forms;
using Autofac;
using Serilog;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Common.Extensions;
using Xperters.Core.Logging;
using ILogger = Xperters.Core.Logging.ILogger;

namespace Xperters.Admin.UI
{
    static class Program
    {

		internal static readonly Lazy<ILogger> LoggerSingleton = new Lazy<ILogger>(ConfigureLogging);
        private static readonly string Version = typeof(Program).Assembly.GetInformationalVersion();
		private static ILogger _log = new NullLogger();



		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        static void Main()
        {
			AppDomain.CurrentDomain.UnhandledException += (sender, args) => HandleUnhandledException(args.ExceptionObject as Exception);
			Application.ThreadException += (sender, args) => HandleUnhandledException(args.Exception);

			try
			{
				using (var container = ContainerOperations.Container)
				{
					_log = LoggerSingleton.Value;
					LogAutofacRegistrations(container);
					_log.Information("Starting {AppName} v{AppVersion} application", typeof(Program).Assembly.GetName().Name, Version);

					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new FormMain());
				}
			}
			catch (Exception ex)
			{
				_log = LoggerSingleton.Value;
				_log.Error(ex, "Starting {AppName} v{AppVersion} application has failed", typeof(Program).Assembly.GetName().Name, Version);

				try
				{
					//TODO: Log Tab
					//var errorForm = new ErrorForm();
					//errorForm.ShowExceptionDialog(ex);
				}
				catch (Exception anotherException)
				{
					_log.Error(anotherException, "Secondary error while trying to show original error - Starting {AppName} v{AppVersion} application has failed", typeof(Program).Assembly.GetName().Name, Version);
					throw;
				}
			}
        }

		private static ILogger ConfigureLogging()
		{
			var envName = ConfigurationManager.AppSettings["xpertersAdmin:environment-name"] ?? "Missing Environment Name setting";
			return new LoggerConfiguration()
				.ReadFrom.AppSettings(settingPrefix: "Xperters")
				.Enrich.WithProperty("AppName", typeof(Program).Assembly.GetName().Name)
				.Enrich.WithProperty("AppVersion", Version)
				.Enrich.WithEnvironmentUserName()
				.Enrich.WithMachineName()
				.Enrich.WithProcessId()
				.Enrich.WithProperty("ProcessUid", Guid.NewGuid())
				.Enrich.WithProperty("EnvName", envName)
				.Enrich.FromLogContext()
				.CreateLogger()
				.ToXpertersILogger();
		}

		private static void LogAutofacRegistrations(IContainer container)
		{
			foreach (var registration in container.ComponentRegistry.Registrations)
			{
				foreach (var service in registration.Services)
				{
					_log.Debug("Autofac: {dependency} resolves to {implementation}, {lifetime}, {sharing}, {ownership}",
						service, registration.Activator.LimitType, registration.Lifetime, registration.Sharing, registration.Ownership);
				}
			}
		}

		private static void HandleUnhandledException(Exception ex)
		{
			_log = LoggerSingleton.Value;
			_log.Error(ex, "Unhandled exception in {AppName} v{AppVersion}.", typeof(Program).Assembly.GetName().Name, Version);

			try
			{
				Application.OpenForms[0].SafelyUpdateControl(() =>
				{
					//TODO: Add to Log Tab
					//var errorForm = new ErrorForm();
					//errorForm.ShowExceptionDialog(ex);
				});
			}
			catch (Exception errorFormException)
			{

				_log.Error(errorFormException, "Unhandled exception in {AppName} v{AppVersion}.", typeof(Program).Assembly.GetName().Name, Version);
				//We swallow this on purpose as to not crash the application
			}

			Application.Exit();
		}

	}
}
