using System;
using Autofac;

namespace Xperters.Admin.UI.Common
{
	public class ContainerOperations
	{
		private static readonly Lazy<IContainer> _containerSingleton = new Lazy<IContainer>(CreateContainer);

		public static bool UseAzureWireup { get; set; }

		public static IContainer Container => _containerSingleton.Value;

		private static IContainer CreateContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new GlobalAutofacModule());

			AddExtraModulesCallBack?.Invoke(builder); // DLS

			return builder.Build();
		}

		public static Action<ContainerBuilder> AddExtraModulesCallBack { get; set; } // DLS
	}
}
