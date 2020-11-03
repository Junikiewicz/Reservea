using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservea.Microservices.Users.Interfaces.Services
{
    public interface IAuthService
    {
        Task Register(string email, string password, string firstName, string lastName);

        Task<string> Login(string email, string password);
    }
}
