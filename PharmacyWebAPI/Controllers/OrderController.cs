﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using PharmacyWebAPI.Models;
using PharmacyWebAPI.Models.Dto;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;

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
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _unitOfWork.Order.GetFirstOrDefaultAsync(p => p.Id == id);
            if (obj is null)
                return NotFound();
            await _unitOfWork.OrderDetail.GetAllAsync(f => f.OrderId == obj.Id);
            return Ok(new { Order = obj });
        }

        [HttpGet]
        [Route("GetUserOrders/{id}")]
        public async Task<IActionResult> GetUserOrders(string id)
        {
            var obj = await _unitOfWork.Order.GetAllAsync(p => p.UserId == id);
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

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return Ok(new { Order = new OrderDto() });
        }

        [HttpPost]
        [Route("Checkout")]
        public async Task<IActionResult> Checkout(/*IEnumerable<OrderDetail> Drugs*/[FromForm] ResponseURLsDto URLs)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(new { State = ModelState, OrderDetails = Drugs });
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }*/
            var Drugs = new List<OrderDetailsDto>();
            List<Drug> drugs = new List<Drug>();

            Drugs.Add(new OrderDetailsDto
            {
                Count = 3,
                DrugId = 5,
                Price = 15,
            });
            Drugs.Add(new OrderDetailsDto
            {
                Count = 2,
                DrugId = 32,
                Price = 20,
            });
            Drugs.Add(new OrderDetailsDto
            {
                Count = 1,
                DrugId = 27,
                Price = 50,
            });

            var orderDetails = _mapper.Map<IEnumerable<OrderDetail>>(Drugs).ToList();
            var order = new Order { UserId = "9b460198-eb98-4c3d-8457-ef6976fa53d5" /*user.Id*/ };
            await _unitOfWork.Order.AddAsync(order);
            await _unitOfWork.SaveAsync();
            order.OrderTotal = _unitOfWork.Order.GetTotalPrice(orderDetails);
            await _unitOfWork.OrderDetail.SetOrderId(order.Id, orderDetails);
            var session = await _unitOfWork.Order.StripeSetting(order, orderDetails, URLs);
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [HttpPost]
        [Route("Checkout2")]
        public async Task<IActionResult> Checkout2([FromForm] List<OrderDetailsDto> Drugs, [FromForm] ResponseURLsDto URLs)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(new { State = ModelState, OrderDetails = Drugs });
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }*/

            var orderDetails = _mapper.Map<IEnumerable<OrderDetail>>(Drugs).ToList();
            var order = new Order { UserId = "9b460198-eb98-4c3d-8457-ef6976fa53d5" /*user.Id*/ };
            await _unitOfWork.Order.AddAsync(order);
            await _unitOfWork.SaveAsync();
            order.OrderTotal = _unitOfWork.Order.GetTotalPrice(orderDetails);
            await _unitOfWork.OrderDetail.SetOrderId(order.Id, orderDetails);
            var session = await _unitOfWork.Order.StripeSetting(order, orderDetails, URLs);
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
        [Route("OrderConfirmation/{id}")]
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

            return Ok(new { success = true, message = "Order Confirm Successfully", OrderId = id });
        }

        [HttpGet]
        [Route("Denied/{id}")]
        public async Task<IActionResult> Denied(int id)
        {
            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(d => d.Id == id);
            _unitOfWork.Order.Delete(order);
            await _unitOfWork.SaveAsync();
            return Ok(new { success = false, message = "Order Denied" });
        }

        /*   [HttpPut]
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
    }
}