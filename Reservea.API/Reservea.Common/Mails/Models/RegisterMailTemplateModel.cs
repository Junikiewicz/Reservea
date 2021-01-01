namespace Reservea.Common.Mails.Models
{
    public class RegisterMailTemplateModel : BaseMailModel
    {
        public string Name { get; set; }
        public string ActivationUrl { get; set; }
    }
}
