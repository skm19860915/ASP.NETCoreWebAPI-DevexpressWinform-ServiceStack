using Microsoft.Extensions.Logging;
using RazorLight;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace xperters.email.TemplateHelper
{
  public class RazorParser
    {
        private Assembly _assembly;
       
        private readonly ILogger _logger;

        public RazorParser(Assembly assembly, ILoggerFactory loggerFactory)
        {
           _assembly = assembly;
            _logger = loggerFactory.CreateLogger<RazorParser>();
        }

        

        public string Parse<T>(string template, T model)
        {
            _logger.LogDebug($"template is in parse method:{template}");
            return ParseAsync(template, model).GetAwaiter().GetResult();
        }       

        public string UsingTemplateFromEmbedded<T>(string path, T model)
        {
            var template = EmbeddedResourceHelper.GetResourceAsString(_assembly, GenerateFileAssemblyPath(path, _assembly));
            _logger.LogDebug($"template is in UsingTemplateFromEmbedded method:{template}");
            var result = Parse(template, model);
            return result;
        }

        async Task<string> ParseAsync<T>(string template, T model)
        {
            var project = new InMemoryRazorLightProject();
            _logger.LogDebug($"InMemoryRazorLightProject is :{project}");
            var engine = new EngineFactory().Create(project);
            return await engine.CompileRenderAsync<T>(Guid.NewGuid().ToString(), template, model);
        }

        string GenerateFileAssemblyPath(string template, Assembly assembly)
        {
            _logger.LogDebug($"Assembly is:{assembly.Location}");
            string assemblyName = assembly.GetName().Name;
            _logger.LogDebug($"assemblyName is:{assemblyName}");
            return string.Format("{0}.{1}.{2}", assemblyName, template, "cshtml");
        }
    }
}
