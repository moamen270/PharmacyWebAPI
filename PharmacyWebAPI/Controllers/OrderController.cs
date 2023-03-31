using AutoMapper;
using PharmacyWebAPI.Models.Dto;
using Stripe.Checkout;

namespace PharmacyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var obj = await _unitOfWork.Order.GetAsync(id);
            if (obj is null)
                return NotFound();
            return Ok(new { Order = obj });
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Order> obj = await _unitOfWork.Order.GetAllAsync();
            return Ok(new { Order = obj });
        }

        [HttpPost]
        [Route("QuickCreate")]
        public async Task<IActionResult> QuickCreate()
        {
            await _unitOfWork.Order.AddAsync(new Order
            {
                UserId = (await _userManager.GetUserAsync(User)).Id,
            });
            await _unitOfWork.SaveAsync();
            return Ok(new { success = true, message = "Order Created Successfully" });
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return Ok(new { Order = new OrderDto() });
        }

        /* [HttpPost]
         [Route("Checkout")]
         public async Task<IActionResult> Checkout(IEnumerable<OrderDetailsDto> Products)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(Products);
             }
             var products = Products.ToList();

             Order order = new Order();
             order.UserId = (await _userManager.GetUserAsync(User)).Id;
             order.PaymentStatus = SD.PaymentStatusPending;
             order.OrderStatus = SD.StatusApproved;

             await _unitOfWork.Order.AddAsync(order);
             _unitOfWork.Save();

             List<OrderDetail> orderDetails = new();
             double totalPrice = 0;
             foreach (var product in products)
             {
                 OrderDetail detail = _mapper.Map<OrderDetail>(product);
                 detail.OrderId = order.Id;
                 await _unitOfWork.OrderDetail.AddAsync(detail);
                 totalPrice += detail.Price * detail.Count;
                 orderDetails.Add(detail);
             }
             order.OrderTotal = totalPrice;
             _unitOfWork.Order.Update(order);
             _unitOfWork.Save();

             //stripe settings
             var domain = "https://localhost:44332/";
             var options = new SessionCreateOptions
             {
                 PaymentMethodTypes = new List<string>
                 {
                   "card",
                 },
                 LineItems = new List<SessionLineItemOptions>(),
                 Mode = "payment",
                 SuccessUrl = domain + $"api/Order/OrderConfirmation?id={order.Id}",
                 CancelUrl = domain + $"api/Order/Denied",
             };

             foreach (var item in orderDetails)
             {
                 var product = await _unitOfWork.Product.GetFirstOrDefaultAsync(p => p.Id == item.ProductId);
                 var sessionLineItem = new SessionLineItemOptions
                 {
                     PriceData = new SessionLineItemPriceDataOptions
                     {
                         UnitAmount = (long)(item.Price * 100),//20.00 -> 2000
                         Currency = "usd",
                         ProductData = new SessionLineItemPriceDataProductDataOptions
                         {
                             Name = product.EnglishName,
                             Images = new List<string>
                             {
                                 product.ImgURL
                             },
                             Description = product.Description
                         },
                     },
                     Quantity = item.Count,
                 };
                 options.LineItems.Add(sessionLineItem);
             }

             var service = new SessionService();
             Session session = service.Create(options);

             _unitOfWork.Order.UpdateStripePaymentID(order.Id, session.Id, session.PaymentIntentId);
             _unitOfWork.Save();
             Response.Headers.Add("Location", session.Url);
             return new StatusCodeResult(303);
         }*/

        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout(IEnumerable<OrderDetailsDto> products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(products);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var order = new Order
            {
                UserId = user.Id,
                PaymentStatus = SD.PaymentStatusPending,
                OrderStatus = SD.StatusApproved
            };

            var orderDetails = new List<OrderDetail>();
            double totalPrice = 0;

            foreach (var product in products)
            {
                var detail = _mapper.Map<OrderDetail>(product);
                detail.OrderId = order.Id;
                await _unitOfWork.OrderDetail.AddAsync(detail);
                totalPrice += detail.Price * detail.Count;
                orderDetails.Add(detail);
            }

            order.OrderTotal = totalPrice;
            await _unitOfWork.Order.AddAsync(order);

            var domain = "https://localhost:44332";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{domain}/api/Order/OrderConfirmation?id={order.Id}",
                CancelUrl = $"{domain}/api/Order/Denied"
            };

            foreach (var item in orderDetails)
            {
                var drug = await _unitOfWork.Drug.GetFirstOrDefaultAsync(p => p.Id == item.DrugId);
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100), // 20.00 -> 2000
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = drug.Name,
                            Images = new List<string> { drug.ImgURL },
                            Description = drug.Description
                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }

            var session = await new SessionService().CreateAsync(options);

            order.SessionId = session.Id;
            order.PaymentIntentId = session.PaymentIntentId;

            _unitOfWork.Order.Update(order);
            await _unitOfWork.SaveAsync();

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _unitOfWork.Order.GetFirstOrDefaultAsync(u => u.Id == id);
            if (obj == null)
                return NotFound(new { success = false, message = "NotFound" });

            _unitOfWork.Order.Delete(obj);
            await _unitOfWork.SaveAsync();

            return Ok(new { success = true, message = "Order Deleted Successfully" });
        }

        [HttpGet]
        [Route("OrderConfirmation")]
        public async Task<IActionResult> OrderConfirmation(int id)
        {
            Order order = await _unitOfWork.Order.GetFirstOrDefaultAsync(u => u.Id == id);
            if (order.PaymentStatus != SD.PaymentStatusPending)
            {
                var service = new SessionService();
                Session session = service.Get(order.SessionId);
                //check the stripe status
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unitOfWork.Order.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                    await _unitOfWork.SaveAsync();
                }
            }
            //_emailSender.SendEmailAsync(orderHeader.ApplicationUser.Email, "New Order - Pharmacy App", "<p>New Order Created</p>");

            return Ok(new { success = true, message = "Order Confirmation Successfully", Order = id });
        }

        [HttpGet]
        [Route("Denied")]
        public IActionResult Denied()
        {
            return Ok(new { success = false, message = "Order Denied" });
        }

        /*  //POST
          [HttpPost]
          [Route("Edit")]
          public async Task<IActionResult> Edit(OrderDto obj)
          {
              if (!ModelState.IsValid)
              {
                  return BadRequest(new { State = ModelState, Order = obj });
              }

              _unitOfWork.Order.Update(_mapper.Map<Order>(obj));
              await _unitOfWork.SaveAsync();
              return Ok(new { success = true, message = "Order Updated Successfully", Order = obj });
          }*/

        /*  public Order GenerateOrder()
          {
              var order = new Order
              {
                  UserId = "73a7b2fe-8769-4a55-960c-9370c95c450e",
                  PaymentStatus = SD.PaymentStatusPending,
                  OrderStatus = SD.StatusApproved
              };
              return order;
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
          }*/

        /*  public async Task SetOptionsValues(SessionCreateOptions options, List<OrderDetail> orderDetails)
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
          }*/
        /*
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
                  */ /* }
                }*/
    }
}