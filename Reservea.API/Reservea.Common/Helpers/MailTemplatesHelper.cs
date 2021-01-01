using RazorEngine;
using RazorEngine.Templating;
using Reservea.Common.Interfaces;
using System;
using System.IO;

namespace Reservea.Common.Helpers
{
    public class MailTemplatesHelper : IMailTemplatesHelper
    {
        private readonly string _rootPath;

        public MailTemplatesHelper()
        {
            _rootPath = Path.Combine(AppContext.BaseDirectory, "Mails\\Templates");
        }

        public string GetTemplateString(string templateName, object model)
        {
            string templateFilePath = Path.Combine(_rootPath, templateName + ".cshtml");
            string template = File.ReadAllText(templateFilePath, System.Text.Encoding.UTF8);

            var result = Engine.Razor.RunCompile(template, templateName, null, model);

            return result[result.IndexOf("<!DOCTYPE html>")..]; //namespace bug workaround
        }
    }
}