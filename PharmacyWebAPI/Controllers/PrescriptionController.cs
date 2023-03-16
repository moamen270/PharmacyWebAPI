using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmacyWebAPI.DataAccess.Repository.IRepository;
using PharmacyWebAPI.Models;
using PharmacyWebAPI.Models.ViewModels;
using System.Diagnostics;

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
            var obj = await  _unitOfWork.Prescription.GetAsync(id);
            if(obj is null)
            return BadRequest("Not Found");
            return Ok(obj);
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(int id)
        {
            /*&& x.DoctorId == (await _userManager.GetUserAsync(User)).Id*/
            var docId = (await _userManager.GetUserAsync(User)).Id;
            IEnumerable<Prescription> obj = await _unitOfWork.Prescription.GetAllAsync(d => d.DoctorId == docId, x => x.Patient);
            return Ok(obj);
        }

        [HttpGet]
        [Route("QuickCreate")]
        public IActionResult QuickCreate()
        {
            return Ok(new Prescription());
        }

        [HttpPost]
        [Route("QuickCreate")]
        public async Task<IActionResult> QuickCreate(string CustId)
        {
            await _unitOfWork.Prescription.AddAsync(new Prescription
            {
                DoctorId = (await _userManager.GetUserAsync(User)).Id,
                PatientId = CustId,
            });
            _unitOfWork.Save();
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
            _unitOfWork.Save();
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
            _unitOfWork.Save();

            return Ok(new { success = true, message = "Prescription Deleted Successfully" });
        }

        //POST
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(PrescriptionDto obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(obj);
            }

            _unitOfWork.Prescription.Update(_mapper.Map<Prescription>(obj));
            _unitOfWork.Save();
            return Ok(obj);
        }
    }
}