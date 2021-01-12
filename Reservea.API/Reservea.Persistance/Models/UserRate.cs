namespace Reservea.Persistance.Models
{
    public class UserRate
    {
        public int Id { get; set; }
        public string Feedback { get; set; }
        public int UserId { get; set; }
        public bool IsVisible { get; set; }
        public bool IsAllowedToBeShared { get; set; }

        public User User { get; set; }
    }
}
