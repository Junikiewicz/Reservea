namespace Reservea.Microservices.Users.Dtos.Requests
{
    public class ResetPasswordRequest
    {
        public string NewPassword { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
