using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PharmacyWebAPI.Models.ViewModels
{
    public class ProductFormDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Empty";
        public string Description { get; set; } = "Empty";

        [Range(1, 1000)]
        public double ListPrice { get; set; } = 1;

        [Range(1, 1000)]
        public double Price { get; set; } = 1;

        [Range(0, 5)]
        public int Rate { get; set; }

        public string ImgURL { get; set; }

        public string? Comment { get; set; }
        public int BrandId { get; set; }

        [ValidateNever]
        public Brand? Brand { get; set; }

        public int CategoryId { get; set; }

        [ValidateNever]
        public Category? Category { get; set; }

        [ValidateNever]
        public IEnumerable<Brand>? Brands { get; set; }

        [ValidateNever]
        public IEnumerable<Category>? Categories { get; set; }

        [ValidateNever]
        public IEnumerable<Rating> Rates { get; set; }

        [ValidateNever]
        public IEnumerable<Comment> Comments { get; set; }
    }
}