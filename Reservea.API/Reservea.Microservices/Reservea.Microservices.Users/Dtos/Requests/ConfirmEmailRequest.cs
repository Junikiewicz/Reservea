namespace Reservea.Microservices.Users.Dtos.Requests
{
    public class ConfirmEmailRequest
    {
        public string Token { get; set; }
        public int Id { get; set; }
    }
}
