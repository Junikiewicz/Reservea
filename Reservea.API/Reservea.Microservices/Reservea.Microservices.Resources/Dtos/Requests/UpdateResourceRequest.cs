namespace Reservea.Microservices.Resources.Dtos.Requests
{
    public class UpdateResourceRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerHour { get; set; }
        public int ResourceTypeId { get; set; }
        public int ResourceStatusId { get; set; }
    }
}