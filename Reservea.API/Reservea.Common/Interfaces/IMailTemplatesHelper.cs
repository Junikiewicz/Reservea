namespace Reservea.Common.Interfaces
{
    public interface IMailTemplatesHelper
    {
        string GetTemplateString(string templateName, object model);
    }
}
