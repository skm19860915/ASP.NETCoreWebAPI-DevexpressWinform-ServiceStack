using System.Collections.Generic;
using xperters.configurations.Interfaces;

namespace xperters.tests.common
{
    public class EnvironmentHandlerMock : IHandleEnvironment
    {
        private readonly Dictionary<string, string> _mockEnvironment;

        public EnvironmentHandlerMock(Dictionary<string, string> mockEnvironment)
        {
            _mockEnvironment = mockEnvironment;
        }

        public string GetVariable(string variableName)
        {
             _mockEnvironment.TryGetValue(variableName, out var result);
             return result;
        }

        public void SetVariable(string variableName, string value)
        {
            // Check for entry not existing and add to dictionary
            _mockEnvironment[variableName] = value;
        }
    }
}
