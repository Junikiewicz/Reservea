using RazorLight;
using Reservea.Common.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Reservea.Common.Helpers
{
    public class MailTemplatesHelper : IMailTemplatesHelper
    {
        private readonly RazorLightEngine _engine;

        public MailTemplatesHelper()
        {
            var rootPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "Mails/Templates").Substring(6);

            if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
            {
                rootPath = '/' + rootPath; //set as absolute path
            }

            _engine = new RazorLightEngineBuilder()
               .UseFileSystemProject(rootPath)
               .UseMemoryCachingProvider()
               .Build();
        }

        public async Task<string> GetTemplateString(string templateName, object model)
        {
            var result = await _engine.CompileRenderAsync($"{templateName}.cshtml", model);

            return result;
        }
    }
}