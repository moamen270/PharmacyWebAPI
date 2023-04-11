using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace PharmacyWebAPI.DataAccess.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);

        void UpdateStripePaymentID(int id, string paymentItentId);

        double GetTotalPrice(List<OrderDetail> Drugs);

        Task<Session> StripeSetting(Order order, List<OrderDetail> orderDetails);

        SessionCreateOptions GenerateOptions(int OrderId);

        Task SetOptionsValues(SessionCreateOptions options, List<OrderDetail> orderDetails);
    }
}