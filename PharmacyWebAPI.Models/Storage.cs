namespace PharmacyWebAPI.Models
{

    public class Storage
    {
        public int Id { get; set; }
        public StorageType Type { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}