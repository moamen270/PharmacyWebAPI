namespace PharmacyWebAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = "New Category";
        public string ImgURL { get; set; } = string.Empty;
    }
}