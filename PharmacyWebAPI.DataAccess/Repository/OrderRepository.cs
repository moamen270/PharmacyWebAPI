using Azure;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PharmacyWebAPI.Models;
using PharmacyWebAPI.Models.Dto;
using Stripe.Checkout;

namespace PharmacyWebAPI.DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _context.Order.FirstOrDefault(u => u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public double GetTotalPrice(List<OrderDetail> Drugs)
        {
            double totalPrice = 0;
            foreach (var drug in Drugs)
            {
                totalPrice += drug.Price * drug.Count;
            }
            return totalPrice;
        }

        public async Task<Session> StripeSetting(Order order, List<OrderDetail> orderDetails)
        {
            var options = GenerateOptions(order.Id);
            await SetOptionsValues(options, orderDetails);

            var session = await new SessionService().CreateAsync(options);

            order.SessionId = session.Id;
            order.PaymentIntentId = session.PaymentIntentId;

            _context.Order.Update(order);
            await _context.SaveChangesAsync();
            return session;
        }

        public SessionCreateOptions GenerateOptions(int OrderId)
        {
            var domain = "https://localhost:44332";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{domain}/Order/OrderConfirmation/{OrderId}",
                CancelUrl = $"{domain}/Order/Denied"
            };
            return options;
        }

        public async Task SetOptionsValues(SessionCreateOptions options, List<OrderDetail> orderDetails)
        {
            foreach (var item in orderDetails)
            {
                var drug = await _context.Drugs.FirstOrDefaultAsync(d => d.Id == item.DrugId);
                if (drug is not null)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100), // 20.00 -> 2000
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = drug.Name,
                                Images = new List<string> { drug.ImageURL },
                                Description = drug.Description,
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                    drug.Stock -= item.Count;
                    _context.Drugs.Update(drug);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public void UpdateStripePaymentID(int id, string paymentItentId)
        {
            var orderFromDb = _context.Order.FirstOrDefault(u => u.Id == id);
            if (orderFromDb is not null)
            {
                orderFromDb.PaymentDate = DateTime.Now;
                orderFromDb.PaymentIntentId = paymentItentId;
            }
        }
    }
}