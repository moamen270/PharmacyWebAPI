namespace PharmacyWebAPI.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public double Rate { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}