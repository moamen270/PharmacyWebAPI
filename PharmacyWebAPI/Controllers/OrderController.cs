using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmacyWebAPI.DataAccess.Repository.IRepository;
using PharmacyWebAPI.Models;
using PharmacyWebAPI.Models.ViewModels;
using System.Diagnostics;

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
                return BadRequest("Not Found");
            return Ok(obj);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            /*&& x.DoctorId == (await _userManager.GetUserAsync(User)).Id*/
            /*var userid = (await _userManager.GetUserAsync(User)).Id;*/
            IEnumerable<Order> obj = await _unitOfWork.Order.GetAllAsync(/*x=>x.UserId == userid*/);
            return Ok(obj);
        }

        [HttpPost]
        [Route("QuickCreate")]
        public async Task<IActionResult> QuickCreate()
        {
            await _unitOfWork.Order.AddAsync(new Order
            {
                UserId = (await _userManager.GetUserAsync(User)).Id,
            });
            _unitOfWork.Save();
            return Ok(new { success = true, message = "Order Created Successfully" });
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return Ok(new OrderDto());
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(OrderDto viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(viewModel);
            }
            viewModel.UserId = (await _userManager.GetUserAsync(User)).Id;
            await _unitOfWork.Order.AddAsync(_mapper.Map<Order>(viewModel));
            _unitOfWork.Save();
            return Ok(new { success = true, message = "Order Created Successfully", viewModel });
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _unitOfWork.Order.GetAsync(id);
            if (obj == null)
                return BadRequest(new { success = false, message = "Error While Deleting" });

            _unitOfWork.Order.Delete(obj);
            _unitOfWork.Save();

            return Ok(new { success = true, message = "Order Deleted Successfully" });
        }

        //POST
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(OrderDto obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(obj);
            }

            _unitOfWork.Order.Update(_mapper.Map<Order>(obj));
            _unitOfWork.Save();
            return Ok(obj);
        }
    }
}