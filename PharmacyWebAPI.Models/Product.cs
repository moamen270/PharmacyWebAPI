namespace PharmacyWebAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ArabicName { get; set; } = string.Empty;
        public string EnglishName { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public string Contraindications { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ExpDate { get; set; }
        public int Stock { get; set; }
        public double ListPrice { get; set; } = 1;
        public double Price { get; set; } = 1;
        public string ImgURL { get; set; } = string.Empty;
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int StorageId { get; set; }
        public Storage Storage { get; set; }
        public IEnumerable<Rating> Rates { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}