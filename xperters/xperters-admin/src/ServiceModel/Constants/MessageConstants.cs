	namespace Xperters.Admin.ServiceModel.Constants
	{
		public static class MessageConstants
		{
			// complies with table for type migration; see https://docs.servicestack.net/openapi
			public static class DataTypes
			{
				public const string Array = "array";
				public const string Boolean = "boolean";
				public const string Integer = "integer";
				public const string Number = "number";
				public const string String = "string";
			}

			public static class DataTypeFormats
			{
				public const string Byte = "int";
				public const string Date = "date";
				public const string DateTime = "date-time";
				public const string Double = "double";
				public const string Float = "float";
				public const string Int32 = "int32";
				public const string Int64 = "int64";
			}

			public static class RequestTypes
			{
				public const string Post = "POST";
				public const string Get = "GET";
				public const string GetPost = "GET,POST";
				public const string PostPut = "POST,PUT";
				public const string Delete = "DELETE";
			}

			internal static class RoutingTags
			{
				internal const string General = "General";
			}

			public static class Routes
			{
				internal const string FinancialPerformanceCycle = "/fas/financial-performance-cycle";
				internal const string FinancialPerformanceCycleFind = "/fas/financial-performance-cycle/find";
				internal const string FinancialPerformanceCycleCreate = "/fas/financial-performance-cycle/create";

				internal const string AttributionMap = "/fas/attribution-map";
				internal const string AttributionMapFind = "/fas/attribution-map/find";
				internal const string AttributionMapCreate = "/fas/attribution-map/create";

				internal const string FinancialPerformanceCycleInputSelection = "/fas/financial-performance-cycle-input-selection";
				internal const string FinancialPerformanceCycleInputSelectionFind = "/fas/financial-performance-cycle-input-selection/find";

				internal const string InputType = "/fas/input-type";
				internal const string InputTypeFind = "/fas/input-type/find";

				internal const string InputHeader = "/fas/input-header";
				internal const string InputHeaderFind = "/fas/input-header/find";

				internal const string OutputType = "/fas/output-type";
				internal const string OutputTypeFind = "/fas/output-type/find";

				internal const string OutputHeader = "/fas/output-header";
				internal const string OutputHeaderFind = "/fas/output-header/find";

				internal const string OutputHeaderInputsUsed = "/fas/output-header-inputs-used";
				internal const string OutputHeaderInputsUsedFind = "/fas/output-header-inputs-used/find";

				public static class AzureActiveDirectory
				{
					//TODO: change to FAS approvers
					internal const string GetTradersRequest = "/fas/aad/trader";
					internal const string GetUserRolesRequest = "/fas/aad/get-user-role";
					internal const string GetUserRequest = "/fas/aad/get-user";
				}
			}


		}
	}
