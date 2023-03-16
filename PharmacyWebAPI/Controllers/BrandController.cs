using Microsoft.AspNetCore.Mvc;
using PharmacyWebAPI.Models;
using PharmacyWebAPI.DataAccess.Repository.IRepository;
using System.Diagnostics;
using PharmacyWebAPI.Models.ViewModels;

namespace PharmacyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            Brand obj = await  _unitOfWork.Brand.GetAsync(id);
            if(obj is null)
            return BadRequest("Not Found");
            return Ok(obj);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Brand> obj = await _unitOfWork.Brand.GetAllAsync();
            return Ok(obj);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return Ok(new Brand());
        }

        //POST
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Brand model)
        {
            await _unitOfWork.Brand.AddAsync(model);
            _unitOfWork.Save();

            return Ok(new { success = true, message = "Brand Created Successfully" });
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _unitOfWork.Brand.GetAsync(id);
            if (brand == null)
                return Ok(new { success = false, message = "Error While Deleting" });

            _unitOfWork.Brand.Delete(brand);
            _unitOfWork.Save();

            return Ok(new { success = true, message = "Brand Deleted Successfully" });
        }

        //POST
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(Brand obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(obj);
            }
            _unitOfWork.Brand.Update(obj);
            _unitOfWork.Save();
            return Ok(new { success = true, message = "Brand Updated Successfully" });
        }
    }
}