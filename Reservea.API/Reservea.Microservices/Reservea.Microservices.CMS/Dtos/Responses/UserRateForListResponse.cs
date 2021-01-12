namespace Reservea.Microservices.CMS.Dtos.Responses
{
    public class UserRateForListResponse
    {
        public string Feedback { get; set; }
        public bool IsVisible { get; set; }
        public bool IsAllowedToBeShared { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
