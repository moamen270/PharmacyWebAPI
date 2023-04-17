using AutoMapper;
using PharmacyWebAPI.Models;
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
        [Route("id")]
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
        [Route("Createrang")]
        public async Task<IActionResult> Createrange()
        {
            var drug = new List<Drug>()
            {
                new Drug
                {
                    Name ="RespiClear ",
                    CategoryId = 8,
                    ManufacturerId = 3,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "RespiClear is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },
                new Drug
                {
                    Name ="BreathEase ",
                    CategoryId = 8,
                    ManufacturerId = 5,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "BreathEase is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="DermaGlow" ,
                    CategoryId = 9,
                    ManufacturerId = 4,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "DermaGlow is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="SkinSoothe ",
                    CategoryId = 9,
                    ManufacturerId = 4,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "SkinSoothe is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="FemEase",
                     CategoryId = 10,
                    ManufacturerId = 1,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "FemEase is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="BellaFemme",
                    CategoryId = 10,
                    ManufacturerId = 7,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "BellaFemme is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="OncoGuard" ,
                    CategoryId = 12,
                    ManufacturerId = 2,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "OncoGuard is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="TumorStop" ,
                    CategoryId = 12,
                    ManufacturerId = 20,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "TumorStop is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="Pediatrex",
                    CategoryId = 13,
                    ManufacturerId = 14,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "Pediatrex is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="Kidz Care",
                    CategoryId = 13,
                    ManufacturerId = 10,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "Kidz Care is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="ImmunoShield",
                    CategoryId = 14,
                    ManufacturerId = 17,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "ImmunoShield is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="Immunotrex" ,
                    CategoryId = 14,
                    ManufacturerId = 6,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "Immunotrex is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="FlexiRelief",
                    CategoryId = 15,
                    ManufacturerId = 11,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "FlexiRelief is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="BoneEase",
                    CategoryId = 15,
                    ManufacturerId = 19,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "BoneEase is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="SoberGuard ",
                    CategoryId = 16,
                    ManufacturerId = 12,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "SoberGuard is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="AddictStop ",
                    CategoryId = 16,
                    ManufacturerId = 13,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "AddictStop is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="Eye Clear",
                    CategoryId = 17,
                    ManufacturerId = 8,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "Eye Clear is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="Visio Guard ",
                    CategoryId = 17,
                    ManufacturerId = 21,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "Visio Guard is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="Neuro Shield",
                    CategoryId = 18,
                    ManufacturerId = 5,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "Neuro Shield is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="Brain Ease" ,
                    CategoryId = 18,
                    ManufacturerId = 12,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "Brain Ease is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="Gastro Guard",
                    CategoryId = 19,
                    ManufacturerId = 10,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "Gastro Guard is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="DigestiEase",
                    CategoryId = 19,
                    ManufacturerId = 2,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "DigestiEase is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="ReproCare" ,
                    CategoryId = 20,
                    ManufacturerId = 7,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "ReproCare is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },new Drug
                {
                    Name ="FertiBoost",
                    CategoryId = 20,
                    ManufacturerId = 3,
                    Contraindications ="kidney disease, liver disease, alcoholism",
                    PregnancyCategory = Models.Enums.PregnancyCategory.C,
                    DosageForm = Models.Enums.DosageForm.Tablet,
                    Description = "FertiBoost is a substance, natural or synthetic, that is used to treat, prevent, or diagnose a medical condition or disease in humans or animals, prescribed by a licensed healthcare provider and dispensed by a licensed pharmacist.",
                    DosageStrength = "500mg",
                    SideEffects = "dizziness, headache, cough",
                },
            };

            await _unitOfWork.Drug.AddRangeAsync(drug);
            await _unitOfWork.SaveAsync();
            return Ok(new { success = true, message = "Drug Created Successfully", Drug = drug });
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
        [Route("Delete/id")]
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
        [Route("GetByCategory/id")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            var drugs = await _unitOfWork.Drug.GetAllFilterAsync(x => x.CategoryId == id, c => c.Category, z => z.Manufacturer);
            if (!drugs.Any())
                return NotFound(new { success = false, message = "Not Found" });
            var DrugsDto = _mapper.Map<IEnumerable<DrugDetailsGetDto>>(drugs);
            return Ok(new { Drugs = DrugsDto });
        }

        [HttpGet]
        [Route("GetByManufacturer/id")]
        public async Task<IActionResult> GetByBrand(int id)
        {
            var drugs = await _unitOfWork.Drug.GetAllFilterAsync(x => x.ManufacturerId == id, c => c.Category, z => z.Manufacturer);
            if (!drugs.Any())
                return NotFound(new { success = false, message = "Not Found" });
            var DrugsDto = _mapper.Map<IEnumerable<DrugDetailsGetDto>>(drugs);
            return Ok(new { Drugs = DrugsDto });
        }

        [HttpPost]
        [Route("AddPhoto/id")]
        public async Task<IActionResult> AddPhoto(int id, IFormFile file)
        {
            var drug = await _unitOfWork.Drug.GetFirstOrDefaultAsync(x => x.Id == id);
            if (drug == null)
                return NotFound();
            if (!string.IsNullOrEmpty(drug.ImageId))
            {
                var DeleteResult = await _photoService.DeletePhotoAsync(drug.ImageId);
                if (DeleteResult.Error is not null)
                    return BadRequest(DeleteResult.Error);
            }
            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error != null)
                return BadRequest(result.Error);
            drug.ImageId = result.PublicId;
            drug.ImageURL = result.SecureUrl.AbsoluteUri;
            _unitOfWork.Drug.Update(drug);
            await _unitOfWork.SaveAsync();

            return Ok(drug.ImageURL);
        }
    }
}