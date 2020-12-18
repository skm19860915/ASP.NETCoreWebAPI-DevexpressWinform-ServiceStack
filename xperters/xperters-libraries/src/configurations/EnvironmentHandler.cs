using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using Moq;
using xperters.configurations.Interfaces;

namespace xperters.configurations
{
    public class EnvironmentHandler : IHandleEnvironment
    {

        public string GetVariable(string variableName)
        {
            var environmentVariable = Environment.GetEnvironmentVariable(variableName);
            return environmentVariable;
        }

        public void SetVariable(string variableName, string value)
        {
            // Check for entry not existing and add to dictionary
            Environment.SetEnvironmentVariable(variableName, value);
        }

        public IHostEnvironment GetHostingEnvironment<T>()
        {
            var environment = new Mock<IHostEnvironment>();
            var path = Directory.GetCurrentDirectory();
            environment
                .Setup(e => e.ApplicationName)
                .Returns(typeof(T).GetTypeInfo().Assembly.GetName().Name);

            environment
                .Setup(e => e.ContentRootPath)
                .Returns(path);

            return environment.Object;
        }
    }
}
