﻿using AutoMapper;
using PharmacyWebAPI.Models.Dto;
using PharmacyWebAPI.Utility.Services.IServices;

namespace PharmacyWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DrugController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public DrugController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var drug = await _unitOfWork.Drug.GetFirstOrDefaultAsync(p => p.Id == id);
            if (drug is null)
                return NotFound(new { success = false, message = "Not Found" });
            var obj = _mapper.Map<DrugDetailsGetDto>(drug);

            return Ok(new { Drug = obj });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Drug> Drug = await _unitOfWork.Drug.GetAllAsync(c => c.Category, z => z.Manufacturer);
            var DrugDetailsGetDto = _mapper.Map<IEnumerable<DrugDetailsGetDto>>(Drug);
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
            PostDrugDto PostDrugDto = new()
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

            var drug = _mapper.Map<Drug>(obj);

            await _unitOfWork.Drug.AddAsync(drug);
            await _unitOfWork.SaveAsync();
            return Ok(new { success = true, message = "Drug Created Successfully", Drug = drug });
        }

        //POST
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _unitOfWork.Drug.GetFirstOrDefaultAsync(p => p.Id == id);
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
            var drug = await _unitOfWork.Drug.GetFirstOrDefaultAsync(p => p.Id == id);
            if (drug == null)
                return NotFound(new { success = false, message = "Not Found" });

            var drugDto = _mapper.Map<PostDrugDto>(drug);
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
        [Route("GetByCategory/{id}")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            var drugs = await _unitOfWork.Drug.GetAllFilterAsync(x => x.CategoryId == id, c => c.Category, z => z.Manufacturer);
            if (!drugs.Any())
                return NotFound(new { success = false, message = "Not Found" });
            var DrugsDto = _mapper.Map<IEnumerable<DrugDetailsGetDto>>(drugs);
            return Ok(new { Drugs = DrugsDto });
        }

        [HttpGet]
        [Route("GetByManufacturer/{id}")]
        public async Task<IActionResult> GetByBrand(int id)
        {
            var drugs = await _unitOfWork.Drug.GetAllFilterAsync(x => x.ManufacturerId == id, c => c.Category, z => z.Manufacturer);
            if (!drugs.Any())
                return NotFound(new { success = false, message = "Not Found" });
            var DrugsDto = _mapper.Map<IEnumerable<DrugDetailsGetDto>>(drugs);
            return Ok(new { Drugs = DrugsDto });
        }

        [HttpPost]
        [Route("AddPhoto/{id}")]
        public async Task<IActionResult> AddPhoto(int id, IFormFile file)
        {
            var drug = await _unitOfWork.Drug.GetFirstOrDefaultAsync(x => x.Id == id);
            if (drug == null)
                return NotFound();

            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error != null)
                return BadRequest(result.Error);

            drug.ImgURL = result.Url.ToString();
            _unitOfWork.Drug.Update(drug);
            await _unitOfWork.SaveAsync();

            return Ok(drug.ImgURL);
        }
    }
}