using AutoMapper;
using Newtonsoft.Json;
using PharmacyWebAPI.Models.Dto;

namespace PharmacyWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public PrescriptionController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await _unitOfWork.Prescription.GetFirstOrDefaultAsync(p => p.Id == id);
            if (obj is null)
                return BadRequest("Not Found");
            return Ok(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = (await _userManager.GetUserAsync(User));
            if (user == null)
                return Unauthorized();
            IEnumerable<Prescription> obj = await _unitOfWork.Prescription.GetAllAsync(d => d.DoctorId == user.Id, x => x.Patient);
            return Ok(obj);
        }

        [HttpGet]
        [Route("QuickCreate")]
        public async Task<IActionResult> QuickCreate()
        {
            var user = (await _userManager.GetUserAsync(User));
            if (user == null)
                return Unauthorized();
            var prescription = new Prescription
            {
                DoctorId = user.Id,
            };
            return Ok(new { Prescription = prescription });
        }

        [HttpPost]
        [Route("QuickCreate")]
        public async Task<IActionResult> QuickCreate(string patientId)
        {
            await _unitOfWork.User.GetFirstOrDefaultAsync(p => p.Id == patientId);
            if (User == null)
                return NotFound();
            await _unitOfWork.Prescription.AddAsync(new Prescription
            {
                PatientId = patientId,
            });
            await _unitOfWork.SaveAsync();
            return Ok(new { success = true, message = "Product Created Successfully" });
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return Ok(new PrescriptionDto());
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(PrescriptionDto viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(viewModel);
            }
            if (await _userManager.FindByIdAsync(viewModel.PatientId) == null)
            {
                return BadRequest(viewModel);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(viewModel);
            viewModel.DoctorId = user.Id;
            await _unitOfWork.Prescription.AddAsync(_mapper.Map<Prescription>(viewModel));
            await _unitOfWork.SaveAsync();
            return Ok(new { success = true, message = "Prescription Created Successfully", viewModel });
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _unitOfWork.Prescription.GetFirstOrDefaultAsync(p => p.Id == id); ;
            if (obj == null)
                return BadRequest(new { success = false, message = "Error While Deleting" });

            _unitOfWork.Prescription.Delete(obj);
            await _unitOfWork.SaveAsync();

            return Ok(new { success = true, message = "Prescription Deleted Successfully" });
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(PrescriptionDto obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(obj);
            }

            _unitOfWork.Prescription.Update(_mapper.Map<Prescription>(obj));
            await _unitOfWork.SaveAsync();
            return Ok(obj);
        }

        [HttpPost]
        [Route("Dispensing ")]
        public async Task<IActionResult> Dispensing(int prescriptionId)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { State = ModelState, PrescriptionId = prescriptionId });
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();
            var prescription = await _unitOfWork.Prescription.GetFirstOrDefaultAsync(p => p.Id == prescriptionId);
            if (prescription == null)
                return NotFound();

            var prescriptionDetails = await _unitOfWork.PrescriptionDetails.GetAllFilterAsync(p => p.PrescriptionId == prescription.Id);
            var orderDetails = _unitOfWork.PrescriptionDetails.PrescriptionDetailsToOrderDetails(prescriptionDetails.ToList());

            //var ss = JsonConvert.SerializeObject(orderDetails);

            return Ok(new { success = true, });
        }
    }
}