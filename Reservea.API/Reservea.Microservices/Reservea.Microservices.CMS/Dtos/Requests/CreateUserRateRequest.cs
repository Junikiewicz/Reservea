namespace Reservea.Microservices.CMS.Dtos.Requests
{
    public class CreateUserRateRequest
    {
        public string Feedback { get; set; }
        public bool IsAllowedToBeShared { get; set; }
    }
}
