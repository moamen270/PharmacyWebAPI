namespace PharmacyWebAPI.Models.ViewModels
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "New Brand";
        public string ImgURL { get; set; } = string.Empty;
    }
}