using Reservea.Microservices.CMS.Dtos.Requests;
using Reservea.Microservices.CMS.Dtos.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.CMS.Interfaces.Services
{
    public interface IHomePageService
    {
        Task<IEnumerable<TextFieldContentResponse>> GetAllTextFieldsContents(CancellationToken cancellationToken);
        Task<TextFieldContentResponse> GetTextFieldContent(string name, CancellationToken cancellationToken);
        Task UpdateTextFieldContent(IEnumerable<UpdateTextFieldContentRequest> request, CancellationToken cancellationToken);
    }
}
