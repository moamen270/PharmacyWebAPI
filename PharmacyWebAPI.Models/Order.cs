namespace PharmacyWebAPI.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public double OrderTotal { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string TrackingNumber { get; set; } = string.Empty;
        public string Carrier { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? SessionId { get; set; }
    }
}