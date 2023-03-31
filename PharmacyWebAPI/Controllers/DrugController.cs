using AutoMapper;
using PharmacyWebAPI.Models.Dto;

namespace PharmacyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrugController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public DrugController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var obj = await _unitOfWork.Drug.GetAsync(id);

            if (obj is null)
                return NotFound(new { success = false, message = "Not Found" });
            DrugDetailsGetDto DrugDetailsGetDto = _mapper.Map<DrugDetailsGetDto>(obj);

            return Ok(new { Drug = DrugDetailsGetDto });
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Drug> Drug = await _unitOfWork.Drug.GetAllAsync(c => c.Category, z => z.Manufacturer);
            IEnumerable<DrugDetailsGetDto> DrugDetailsGetDto = _mapper.Map<IEnumerable<DrugDetailsGetDto>>(Drug);
            return Ok(new { Drugs = DrugDetailsGetDto });
        }

        //POST
        [HttpPost]
        [Route("QuickCreate")]
        public async Task<IActionResult> QuickCreate()
        {
            var model = new Drug
            {
                CategoryId = (await _unitOfWork.Category.GetFirstOrDefaultAsync()).Id,
                ManufacturerId = (await _unitOfWork.Manufacturer.GetFirstOrDefaultAsync()).Id
            };
            await _unitOfWork.Drug.AddAsync(model);
            await _unitOfWork.SaveAsync();
            return Ok(new { success = true, message = "Drug Created Successfully", model });
        }

        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            PostDrugDto PostDrugDto = new PostDrugDto
            {
                Categories = await _unitOfWork.Category.GetAllAsync(),
                Manufacturers = await _unitOfWork.Manufacturer.GetAllAsync()
            };
            return Ok(new { Drug = PostDrugDto });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(PostDrugDto obj)
        {
            if (!ModelState.IsValid)
            {
                obj.Categories = await _unitOfWork.Category.GetAllAsync();
                obj.Manufacturers = await _unitOfWork.Manufacturer.GetAllAsync();
                return BadRequest(new { State = ModelState, Drug = obj });
            }

            Drug drug = _mapper.Map<Drug>(obj);

            await _unitOfWork.Drug.AddAsync(drug);
            await _unitOfWork.SaveAsync();
            return Ok(new { success = true, message = "Drug Created Successfully", Drug = drug });
        }

        //POST
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _unitOfWork.Drug.GetAsync(id);
            if (obj == null)
            {
                return NotFound(new { success = false, message = "Not Found" });
            }
            _unitOfWork.Drug.Delete(obj);
            await _unitOfWork.SaveAsync();
            return Ok(new { success = true, message = "Drug Deleted Successfully" });
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var drug = await _unitOfWork.Drug.GetAsync(id);
            if (drug == null)
                return NotFound(new { success = false, message = "Not Found" });

            PostDrugDto drugDto = _mapper.Map<PostDrugDto>(drug);
            drugDto.Categories = await _unitOfWork.Category.GetAllAsync();
            drugDto.Manufacturers = await _unitOfWork.Manufacturer.GetAllAsync();

            return Ok(new { Drug = drugDto });
        }

        //Put
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(PostDrugDto obj)
        {
            if (!ModelState.IsValid)
            {
                obj.Categories = await _unitOfWork.Category.GetAllAsync();
                obj.Manufacturers = await _unitOfWork.Manufacturer.GetAllAsync();
                return BadRequest(new { State = ModelState, Drug = obj }); ;
            }

            Drug drug = _mapper.Map<Drug>(obj);

            _unitOfWork.Drug.Update(drug);
            await _unitOfWork.SaveAsync();
            return Ok(new { success = true, message = "Drug Updated Successfully", Drug = drug });
        }

        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> Details(int id)
        {
            Drug drug = await _unitOfWork.Drug.GetFirstOrDefaultAsync(p => p.Id == id);
            if (drug is null)
                return NotFound(new { success = false, message = "Not Found" });
            DrugDetailsGetDto obj = _mapper.Map<DrugDetailsGetDto>(drug);

            return Ok(new { Drug = obj });
        }

        [HttpGet]
        [Route("GetByCategory/{id}")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            var drugs = await _unitOfWork.Drug.GetAllFilterAsync(x => x.CategoryId == id, c => c.Category, z => z.Manufacturer);
            if (drugs.Count() == 0)
                return NotFound(new { success = false, message = "Not Found" });
            IEnumerable<DrugDetailsGetDto> DrugsDto = _mapper.Map<IEnumerable<DrugDetailsGetDto>>(drugs);
            return Ok(new { Drugs = DrugsDto });
        }

        [HttpGet]
        [Route("GetByManufacturer/{id}")]
        public async Task<IActionResult> GetByBrand(int id)
        {
            var drugs = await _unitOfWork.Drug.GetAllFilterAsync(x => x.ManufacturerId == id, c => c.Category, z => z.Manufacturer);
            if (drugs.Count() == 0)
                return NotFound(new { success = false, message = "Not Found" });
            IEnumerable<DrugDetailsGetDto> DrugsDto = _mapper.Map<IEnumerable<DrugDetailsGetDto>>(drugs);
            return Ok(new { Drugs = DrugsDto });
        }
    }
}