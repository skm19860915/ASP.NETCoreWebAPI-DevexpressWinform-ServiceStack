using System;
using System.ComponentModel;

namespace Xperters.Core.Configuration
{
    // ReSharper disable once InconsistentNaming
    public static class IConfigurationManagerExtensions
    {
        public static T GetAppSetting<T>(this IConfigurationManager configurationManager, string key,
            bool optional = false, T defaultValueIfNullOrWhiteSpace = default(T), Func<string, T> customParser = null,
            string customParserErrorMessage = null, Predicate<T> customValidator = null,
            string customValidatorErrorMessage = null)
        {
            if (configurationManager == null)
            {
                throw new ArgumentNullException("configurationManager");
            }

            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            var valueString = configurationManager.AppSettings[key];
            T value;

            if (string.IsNullOrWhiteSpace(valueString))
            {
                if (!optional)
                {
                    throw new InvalidConfigurationException(
                        string.Format("'{0}' is mandatory and there is no value for it in the appSettings", key));
                }

                value = defaultValueIfNullOrWhiteSpace;
            }
            else
            {
                try
                {
                    if (customParser != null)
                    {
                        value = customParser(valueString);
                    }
                    else
                    {
                        value = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(valueString);
                    }
                }
                catch (Exception ex)
                {
                    var messageTemplate = customValidatorErrorMessage ?? "'{value}' is not a valid value for {type}, on {key} in appSettings";

                    var parserErrorMessage = messageTemplate
                        .Replace("{key}", key)
                        .Replace("{value}", valueString)
                        .Replace("{type}", typeof(T).Name);

                    throw new InvalidConfigurationException(parserErrorMessage, ex);
                }
            }

            if (customValidator == null)
            {
                return value;
            }

            try
            {
                if (!customValidator(value))
                {
                    throw new InvalidConfigurationException();
                }
            }
            catch (Exception ex)
            {
                var messageTemplate = customValidatorErrorMessage ?? "'{value}' is not a valid value for {type}, on {key} in appSettings";

                var validatorErrorMessage = messageTemplate
                    .Replace("{key}", key)
                    .Replace("{value}", valueString)
                    .Replace("{type}", typeof(T).Name);

                throw new InvalidConfigurationException(validatorErrorMessage, ex);
            }

            return value;
        }

        public static string GetConnectionString(this IConfigurationManager configurationManager, string name,
            bool optional = false, string defaultValueIfNullOrWhiteSpace = null, Predicate<string> customValidator = null, string customValidatorErrorMessage = null)
        {
            if (configurationManager == null)
            {
                throw new ArgumentNullException("configurationManager");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            var connectionStringInstance = configurationManager.ConnectionStrings[name];
            string connectionString;

            if (connectionStringInstance == null)
            {
                if (!optional)
                {
                    throw new InvalidConfigurationException(
                        string.Format("'{0}' is mandatory and there is no value for it in the connectionStrings", name));
                }

                connectionString = defaultValueIfNullOrWhiteSpace;
            }
            else
            {
                connectionString = connectionStringInstance.ConnectionString;
            }

            if (customValidator == null)
            {
                return connectionString;
            }

            try
            {
                if (!customValidator(connectionString))
                {
                    throw new InvalidConfigurationException();
                }
            }
            catch (Exception ex)
            {
                var validatorErrorMessage = (customValidatorErrorMessage ?? "'{value}' is not a valid value, on {name} in the connectionStrings")
                    .Replace("{name}", name)
                    .Replace("{value}", connectionString);

                throw new InvalidConfigurationException(validatorErrorMessage, ex);
            }

            return connectionString;
        }

        public static T GetSection<T>(this IConfigurationManager configurationManager, string sectionName)
        {
            return (T)configurationManager.GetSection(sectionName);
        }
    }
}
