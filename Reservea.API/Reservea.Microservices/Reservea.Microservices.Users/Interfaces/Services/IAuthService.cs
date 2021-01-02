using Reservea.Common.Helpers;
using Reservea.Microservices.Users.Dtos.Requests;
using Reservea.Microservices.Users.Dtos.Responses;
using System.Threading.Tasks;

namespace Reservea.Microservices.Users.Interfaces.Services
{
    public interface IAuthService
    {
        Task Register(string email, string password, string firstName, string lastName);

        Task<LoginResponse> Login(string email, string password);
        Task ConfirmEmail(ConfirmEmailRequest request);
    }
}
