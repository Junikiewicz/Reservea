namespace Reservea.Common.Exceptions
{
    public class AuthenticationException : ApiException
    {
        public AuthenticationException(string message) : base(message)
        {
        }
    }
}
