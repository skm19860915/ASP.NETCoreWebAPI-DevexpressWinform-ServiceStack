namespace Xperters.Core.Configuration
{
    public interface IConfigurationManager
    {
        /// <summary>
        /// Retrieves a specified configuration section for the current application's default configuration.
        /// </summary>
        /// 
        /// <returns>
        /// The specified <see cref="T:Xperters.Configuration.ConfigurationSection"/> object, or null if the section does not exist.
        /// </returns>
        /// <param name="sectionName">The configuration section path and name.</param>
        /// <exception cref="T:Xperters.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        object GetSection(string sectionName);

        /// <summary>
        /// Refreshes the named section so the next time that it is retrieved it will be re-read from disk.
        /// </summary>
        /// <param name="sectionName">The configuration section name or the configuration path and section name of the section to refresh.</param>
        void RefreshSection(string sectionName);

        /// <summary>
        /// Opens the machine configuration file on the current computer as a <see cref="T:Xperters.Configuration.Configuration"/> object.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:Xperters.Configuration.Configuration"/> object.
        /// </returns>
        /// <exception cref="T:Xperters.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        object OpenMachineConfiguration();

        /// <summary>
        /// Opens the machine configuration file as a <see cref="T:Xperters.Configuration.Configuration"/> object that uses the specified file mapping.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:Xperters.Configuration.Configuration"/> object.
        /// </returns>
        /// <param name="fileMap">An <see cref="T:Xperters.Configuration.ExeConfigurationFileMap"/> object that references configuration file to use instead of the application default configuration file.</param>
        /// <exception cref="T:Xperters.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        object OpenMappedMachineConfiguration(object fileMap);

        /// <summary>
        /// Opens the configuration file for the current application as a <see cref="T:Xperters.Configuration.Configuration"/> object.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:Xperters.Configuration.Configuration"/> object.
        /// </returns>
        /// <param name="userLevel">The <see cref="T:Xperters.Configuration.ConfigurationUserLevel"/> for which you are opening the configuration.</param>
        /// <exception cref="T:Xperters.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        object OpenExeConfiguration(object userLevel);

        /// <summary>
        /// Opens the specified client configuration file as a <see cref="T:Xperters.Configuration.Configuration"/> object.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:Xperters.Configuration.Configuration"/> object.
        /// </returns>
        /// <param name="exePath">The path of the executable (exe) file.</param>
        /// <exception cref="T:Xperters.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        object OpenExeConfiguration(string exePath);

        /// <summary>
        /// Opens the specified client configuration file as a <see cref="T:Xperters.Configuration.Configuration"/> object that uses the specified file mapping and user level.
        /// </summary>
        /// 
        /// <returns>
        /// The configuration object.
        /// </returns>
        /// <param name="fileMap">An <see cref="T:Xperters.Configuration.ExeConfigurationFileMap"/> object that references configuration file to use instead of the application default configuration file.</param>
        /// <param name="userLevel">The <see cref="T:Xperters.Configuration.ConfigurationUserLevel"/> object for which you are opening the configuration.</param>
        /// <exception cref="T:Xperters.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        object OpenMappedExeConfiguration(object fileMap, object userLevel);

        /// <summary>
        /// Opens the specified client configuration file as a <see cref="T:Xperters.Configuration.Configuration"/> object that uses the specified file mapping, user level, and preload option.
        /// </summary>
        /// 
        /// <returns>
        /// The configuration object.
        /// </returns>
        /// <param name="fileMap">An <see cref="T:Xperters.Configuration.ExeConfigurationFileMap"/> object that references the configuration file to use instead of the default application configuration file.</param>
        /// <param name="userLevel">The <see cref="T:Xperters.Configuration.ConfigurationUserLevel"/> object for which you are opening the configuration.</param>
        /// <param name="preLoad">true to preload all section groups and sections; otherwise, false.</param>
        /// <exception cref="T:Xperters.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
        object OpenMappedExeConfiguration(object fileMap, object userLevel, bool preLoad);

        /// <summary>
        /// Gets the <see cref="T:Xperters.Configuration.AppSettingsSection"/> data for the current application's default configuration.
        /// </summary>
        /// 
        /// <returns>
        /// Returns a <see cref="T:System.Collections.Specialized.NameValueCollection"/> object that contains the contents of the <see cref="T:Xperters.Configuration.AppSettingsSection"/> object for the current application's default configuration.
        /// </returns>
        /// <exception cref="T:Xperters.Configuration.ConfigurationErrorsException">Could not retrieve a <see cref="T:System.Collections.Specialized.NameValueCollection"/> object with the application settings data.</exception>
        //NameValueCollection AppSettings { get; }
        IAppSettingsCollection AppSettings { get; } // TODO: Remove comments / update comments

        /// <summary>
        /// Gets the <see cref="T:Xperters.Configuration.ConnectionStringsSection"/> data for the current application's default configuration.
        /// </summary>
        /// 
        /// <returns>
        /// Returns a <see cref="T:Xperters.Configuration.ConnectionStringSettingsCollection"/> object that contains the contents of the <see cref="T:Xperters.Configuration.ConnectionStringsSection"/> object for the current application's default configuration.
        /// </returns>
        /// <exception cref="T:Xperters.Configuration.ConfigurationErrorsException">Could not retrieve a <see cref="T:Xperters.Configuration.ConnectionStringSettingsCollection"/> object.</exception>
        IConnectionStringSettingsCollection ConnectionStrings { get; }
    }
}
