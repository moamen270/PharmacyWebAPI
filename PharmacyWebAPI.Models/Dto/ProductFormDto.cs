using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PharmacyWebAPI.Models.ViewModels
{
    public class ProductFormDto
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

        [ValidateNever]
        public Brand? Brand { get; set; }

        public int CategoryId { get; set; }

        [ValidateNever]
        public Category? Category { get; set; }

        public int StorageId { get; set; }
        public Storage Storage { get; set; }

        [ValidateNever]
        public IEnumerable<Brand>? Brands { get; set; }

        [ValidateNever]
        public IEnumerable<Category>? Categories { get; set; }

        [ValidateNever]
        public IEnumerable<Storage>? Storages { get; set; }
    }
}