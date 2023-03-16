using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PharmacyWebAPI.Models.ViewModels
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Range(1, 1000)]
        public double ListPrice { get; set; } = 1;

        [Range(1, 1000)]
        public double Price { get; set; } = 1;

        public string ImgURL { get; set; }
        public double AvgRate { get; set; }

        [Range(1, 5)]
        [Required]
        public double Rate { get; set; }

        public string Comment { get; set; } = string.Empty;
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [ValidateNever]
        public IEnumerable<Rating> Rates { get; set; }

        [ValidateNever]
        public IEnumerable<Comment> Comments { get; set; }
    }
}