using AutoMapper;
using PharmacyWebAPI.Models.Dto;

namespace PharmacyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var obj = await _unitOfWork.Prescription.GetFirstOrDefaultAsync(p => p.Id == id);
            if (obj is null)
                return BadRequest("Not Found");
            return Ok(obj);
        }

        [HttpGet]
        [Route("GetAll")]
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
            await _unitOfWork.SaveAsynce();
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
            viewModel.DoctorId = (await _userManager.GetUserAsync(User)).Id;
            await _unitOfWork.Prescription.AddAsync(_mapper.Map<Prescription>(viewModel));
            await _unitOfWork.SaveAsynce();
            return Ok(new { success = true, message = "Prescription Created Successfully", viewModel });
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _unitOfWork.Prescription.GetAsync(id);
            if (obj == null)
                return BadRequest(new { success = false, message = "Error While Deleting" });

            _unitOfWork.Prescription.Delete(obj);
            await _unitOfWork.SaveAsynce();

            return Ok(new { success = true, message = "Prescription Deleted Successfully" });
        }

        //POST
        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(PrescriptionDto obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(obj);
            }

            _unitOfWork.Prescription.Update(_mapper.Map<Prescription>(obj));
            await _unitOfWork.SaveAsynce();
            return Ok(obj);
        }

        [HttpPost]
        [Route("Dispens")]
        public async Task<IActionResult> Dispens(Prescription prescription)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { State = ModelState, Prescription = prescription });

            return Ok(new { success = true, });
        }

        /*    private async Task<Order> ConvertPrescriptionToOrder(Prescription prescription)
            {
                var prescriptionDetails = await _unitOfWork.PrescriptionDetails.GetAllFilterAsync(p => p.PrescriptionId == prescription.Id);
            }

            private Task<Order> ConvertPrescriptionDetailsToOrderDetails(Order order, List<PrescriptionDetails> prescriptionDetails)
            {
            }*/
    }
}