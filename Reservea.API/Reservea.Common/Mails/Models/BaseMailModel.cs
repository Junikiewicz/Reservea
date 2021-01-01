namespace Reservea.Common.Mails.Models
{
    public abstract class BaseMailModel
    {
        public string To { get; set; }
        public string ToAddress { get; set; }
        public string Subject { get; set; }
    }
}