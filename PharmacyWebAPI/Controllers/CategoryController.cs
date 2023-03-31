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
            if (!ModelState.IsValid)
            {
                return BadRequest(new { State = ModelState, Category = model });
            }
            await _unitOfWork.Category.AddAsync(model);
            await _unitOfWork.SaveAsynce();

            return Ok(new { success = true, message = "Category Created Successfully", Category = model });
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Category = await _unitOfWork.Category.GetAsync(id);
            if (Category == null)
                return NotFound(new { success = false, message = "NotFound" });

            _unitOfWork.Category.Delete(Category);
            await _unitOfWork.SaveAsynce();

            return Ok(new { success = true, message = "Category Deleted Successfully" });
        }

        //POST
        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(Category obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { State = ModelState, Category = obj });
            }

            _unitOfWork.Category.Update(obj);
            await _unitOfWork.SaveAsynce();
            return Ok(new { success = true, message = "Category Updated Successfully", Category = obj });
        }
    }
}