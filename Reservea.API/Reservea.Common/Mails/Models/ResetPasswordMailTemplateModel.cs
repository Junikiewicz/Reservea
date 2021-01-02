namespace Reservea.Common.Mails.Models
{
    public class ResetPasswordMailTemplateModel : BaseMailModel
    {
        public string Name { get; set; }
        public string ResetPasswordUrl { get; set; }
    }
}
