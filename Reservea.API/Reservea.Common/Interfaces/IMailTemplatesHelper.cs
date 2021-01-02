using System.Threading.Tasks;

namespace Reservea.Common.Interfaces
{
    public interface IMailTemplatesHelper
    {
        Task<string> GetTemplateString(string templateName, object model);
    }
}
