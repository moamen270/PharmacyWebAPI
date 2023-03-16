namespace PharmacyWebAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string text { get; set; } = string.Empty;
        public DateTime AddedDateTime { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}