using PharmacyWebAPI.Models.Enums;

namespace PharmacyWebAPI.Models
{
    public class Drug
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DosageForm DosageForm { get; set; } = DosageForm.Tablet;
        public string DosageStrength { get; set; } = string.Empty;
        public string SideEffects { get; set; } = string.Empty;
        public string Contraindications { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public PregnancyCategory PregnancyCategory { get; set; } = PregnancyCategory.C;
        public int Stock { get; set; } = 20;
        public double Price { get; set; } = GenerateRandomNumber();
        public string ImgURL { get; set; } = string.Empty;

        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Manufacturer Manufacturer { get; set; }
        public Category Category { get; set; }

        private static double GenerateRandomNumber()
        {
            Random random = new Random();
            var number = random.NextDouble() * (100 - 1) + 1;
            var result = double.Parse(number.ToString("00.00"));
            return result;
        }
    }
}