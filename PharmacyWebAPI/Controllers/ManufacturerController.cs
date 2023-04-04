namespace PharmacyWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManufacturerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            Manufacturer obj = await _unitOfWork.Manufacturer.GetFirstOrDefaultAsync(p => p.Id == id);
            if (obj is null)
                return NotFound(new { success = false, message = "Not Found" });
            return Ok(new { Brand = obj });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Manufacturer> obj = await _unitOfWork.Manufacturer.GetAllAsync();
            return Ok(new { Brand = obj });
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return Ok(new { Brand = new Manufacturer() });
        }

        //POST
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Manufacturer model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { State = ModelState, Manufacturer = model });

            await _unitOfWork.Manufacturer.AddAsync(model);
            await _unitOfWork.SaveAsync();

            return Ok(new { success = true, message = "Brand Created Successfully", Brand = model });
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _unitOfWork.Manufacturer.GetFirstOrDefaultAsync(p => p.Id == id);
            if (brand == null)
                return NotFound(new { success = false, message = "NotFound" });

            _unitOfWork.Manufacturer.Delete(brand);
            await _unitOfWork.SaveAsync();

            return Ok(new { success = true, message = "Brand Deleted Successfully" });
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(Manufacturer obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { State = ModelState, Manufacturer = obj });
            }
            _unitOfWork.Manufacturer.Update(obj);
            await _unitOfWork.SaveAsync();
            return Ok(new { success = true, message = "Brand Updated Successfully" });
        }
    }
}