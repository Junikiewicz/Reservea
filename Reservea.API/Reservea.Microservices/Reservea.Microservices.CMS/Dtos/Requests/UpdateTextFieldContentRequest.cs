namespace Reservea.Microservices.CMS.Dtos.Requests
{
    public class UpdateTextFieldContentRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
