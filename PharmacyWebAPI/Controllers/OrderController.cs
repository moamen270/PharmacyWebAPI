using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using PharmacyWebAPI.Models.Dto;
using Stripe;
using Stripe.Checkout;

namespace PharmacyWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _unitOfWork.Order.GetFirstOrDefaultAsync(p => p.Id == id);
            if (obj is null)
                return NotFound();
            return Ok(new { Order = obj });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Order> obj = await _unitOfWork.Order.GetAllAsync();
            return Ok(new { Order = obj });
        }

        [HttpPost]
        [Route("QuickCreate")]
        [Authorize]
        public async Task<IActionResult> QuickCreate()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();
            await _unitOfWork.Order.AddAsync(new Order
            {
                UserId = user.Id,
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

        [HttpPost]
        [Route("Checkout")]
        public async Task<IActionResult> Checkout(IEnumerable<OrderDetailsDto> Drugs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { State = ModelState, OrderDetails = Drugs });
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var orderDetails = _mapper.Map<IEnumerable<OrderDetail>>(Drugs).ToList();
            var order = _unitOfWork.Order.GenerateOrder(user.Id);
            order.OrderTotal = _unitOfWork.Order.GetTotalPrice(orderDetails);
            await _unitOfWork.OrderDetail.SetOrderId(order.Id, orderDetails);
            await _unitOfWork.OrderDetail.AddRangeAsync(orderDetails);

            var session = await _unitOfWork.Order.StripeSetting(order, orderDetails);
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        /* [HttpPost("Checkout")]
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
 */

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

        [HttpPut]
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
        }
    }
}