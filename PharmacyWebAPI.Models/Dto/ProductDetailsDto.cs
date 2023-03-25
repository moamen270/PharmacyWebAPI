using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PharmacyWebAPI.Models.ViewModels
{
    public class ProductDetailDto
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

        public int Quantity { get; set; } = 1;
        public int StorageId { get; set; }
        public Storage Storage { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}