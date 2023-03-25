using Microsoft.AspNetCore.Mvc;
using PharmacyWebAPI.DataAccess.Repository.IRepository;
using PharmacyWebAPI.Models;
using System.Diagnostics;

namespace PharmacyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var obj = await _unitOfWork.Category.GetAsync(id);
            if (obj is null)
                return NotFound(new { success = false, message = "NotFound" });
            return Ok(new { Category = obj });
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Category> obj = await _unitOfWork.Category.GetAllAsync();
            return Ok(new { Categories = obj });
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return Ok(new { Category = new Category() });
        }

        //POST
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Category model)
        {
            await _unitOfWork.Category.AddAsync(model);
            _unitOfWork.Save();

            return Ok(new { success = true, message = "Category Created Successfully", Category = model });
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Category = await _unitOfWork.Category.GetAsync(id);
            if (Category == null)
                return BadRequest(new { success = false, message = "Error While Deleting" });

            _unitOfWork.Category.Delete(Category);
            _unitOfWork.Save();

            return Ok(new { success = true, message = "Category Deleted Successfully" });
        }

        //POST
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Error While Editing", Category = obj });
            }

            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
            return Ok(new { success = true, message = "Category Updated Successfully", Category = obj });
        }
    }
}