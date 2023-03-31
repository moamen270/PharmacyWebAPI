using Azure;
using Microsoft.AspNetCore.Mvc;
using Stripe.BillingPortal;
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

        /*    public Order GenerateOrder()
            {
                var order = new Order
                {
                    UserId = "73a7b2fe-8769-4a55-960c-9370c95c450e",
                    PaymentStatus = PharmacyWebAPI.Utility.SD.PaymentStatusPending,
                    OrderStatus = SD.StatusApproved
                };
                return order;
            }*/

        public double GetTotalPrice(List<OrderDetail> Drugs)
        {
            double totalPrice = 0;
            foreach (var d in Drugs)
            {
                totalPrice += d.Price * d.Count;
            }
            return totalPrice;
        }

        public void SetOrderId(int OrderId, List<OrderDetail> details)
        {
            foreach (var d in details)
            {
                d.OrderId = OrderId;
            }
        }

        /*
                public async Task<StatusCodeResult> StripePrepare(Order order, List<OrderDetail> orderDetails)
                {
                    var options = GenerateOptions(order.Id);
                    await SetOptionsValues(options, orderDetails);

                    var session = await new SessionService().CreateAsync(options);

                    order.SessionId = session.Id;
                    order.PaymentIntentId = session.PaymentIntentId;

                    _context.Order.Update(order);
                    await _context.SaveChangesAsync();

                    Response.Headers.Add("Location", session.Url);
                    return new StatusCodeResult(303);
                }

                public SessionCreateOptions GenerateOptions(int OrderId)
                {
                    var domain = "https://localhost:44332";
                    var options = new SessionCreateOptions
                    {
                        PaymentMethodTypes = new List<string> { "card" },
                        LineItems = new List<SessionLineItemOptions>(),
                        Mode = "payment",
                        SuccessUrl = $"{domain}/api/Order/OrderConfirmation?id={OrderId}",
                        CancelUrl = $"{domain}/api/Order/Denied"
                    };
                    return options;
                }

                public async Task SetOptionsValues(SessionCreateOptions options, List<OrderDetail> orderDetails)
                {
                    foreach (var item in orderDetails)
                    {
                        var product = await _unitOfWork.Drug.GetFirstOrDefaultAsync(p => p.Id == item.DrugId);
                        var sessionLineItem = new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = (long)(item.Price * 100), // 20.00 -> 2000
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = product.Name,
                                    Images = new List<string> { product.ImgURL },
                                    Description = product.Description
                                }
                            },
                            Quantity = item.Count
                        };
                        options.LineItems.Add(sessionLineItem);
                        product.Stock -= item.Count;
                        _unitOfWork.Drug.Update(product);
                    }
                }
        */

        public void UpdateStripePaymentID(int id, string paymentItentId)
        {
            var orderFromDb = _context.Order.FirstOrDefault(u => u.Id == id);
            orderFromDb.PaymentDate = DateTime.Now;
            orderFromDb.PaymentIntentId = paymentItentId;
        }
    }
}