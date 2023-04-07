using AutoMapper;
using PharmacyWebAPI.Models.Dto;
using PharmacyWebAPI.Utility.Services.IServices;

namespace PharmacyWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var drug = await _unitOfWork.Category.GetFirstOrDefaultAsync(p => p.Id == id);
            if (drug is null)
                return NotFound(new { success = false, message = "Not Found" });
            var obj = _mapper.Map<CategoryDto>(drug);

            return Ok(new { Drug = obj });
        }

        [HttpGet]
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
            if (!ModelState.IsValid)
            {
                return BadRequest(new { State = ModelState, Category = model });
            }
            await _unitOfWork.Category.AddAsync(model);
            await _unitOfWork.SaveAsync();

            return Ok(new { success = true, message = "Category Created Successfully", Category = model });
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Category = await _unitOfWork.Category.GetFirstOrDefaultAsync(p => p.Id == id);
            if (Category == null)
                return NotFound(new { success = false, message = "NotFound" });

            _unitOfWork.Category.Delete(Category);
            await _unitOfWork.SaveAsync();

            return Ok(new { success = true, message = "Category Deleted Successfully" });
        }

        //POST
        [HttpGet]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _unitOfWork.Category.GetFirstOrDefaultAsync(p => p.Id == id);
            if (category == null)
                return NotFound();

            return Ok(new { Category = category });
        }

        //POST
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { State = ModelState, Category = obj });
            }

            _unitOfWork.Category.Update(obj);
            await _unitOfWork.SaveAsync();
            return Ok(new { success = true, message = "Category Updated Successfully", Category = obj });
        }

        [HttpPost]
        [Route("AddPhoto/{id}")]
        public async Task<IActionResult> AddPhoto(int id, IFormFile file)
        {
            var category = await _unitOfWork.Category.GetFirstOrDefaultAsync(x => x.Id == id);
            if (category is null)
                return NotFound();

            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error != null)
                return BadRequest(result.Error);

            category.ImgURL = result.Url.ToString();
            _unitOfWork.Category.Update(category);
            await _unitOfWork.SaveAsync();

            return Ok(category.ImgURL);
        }
    }
}