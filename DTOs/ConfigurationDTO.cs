namespace CarConfigurator.DTOs
{
    public class ConfigurationDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public List<int> CarComponentIds { get; set; }
    }
}