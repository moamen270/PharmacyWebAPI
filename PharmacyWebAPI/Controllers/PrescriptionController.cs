using AutoMapper;
using Newtonsoft.Json;
using PharmacyWebAPI.Models;
using PharmacyWebAPI.Models.Dto;
using System.Text;

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
        [Route("{id}")]
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
            /*  var user = (await _userManager.GetUserAsync(User));
              if (user == null)
                  return Unauthorized();*/
            IEnumerable<Prescription> obj = await _unitOfWork.Prescription.GetAllAsync(x => x.Patient, y => y.Doctor);
            var list = obj.Where(x => x.Doctor.Id == "9b460198-eb98-4c3d-8457-ef6976fa53d5");
            return Ok(obj);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(IEnumerable<PrescriptionDetailsDto> Drugs)
        {
            /*
            if (!ModelState.IsValid)
            {
                return BadRequest(drugs);
            }
            if (await _userManager.FindByIdAsync(viewModel.PatientId) == null)
            {
                return BadRequest(drugs);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(drugs);*/

            var prescription = new Prescription() { DoctorId = "9b460198-eb98-4c3d-8457-ef6976fa53d5", PatientId = "f12913db-9cfd-47fe-8969-9d07780b6263", };
            await _unitOfWork.Prescription.AddAsync(prescription);
            await _unitOfWork.SaveAsync();
            var drugs = _mapper.Map<IEnumerable<PrescriptionDetails>>(Drugs).ToList();
            await _unitOfWork.PrescriptionDetails.SetPresciptionId(prescription.Id, drugs);
            return Ok(new { success = true, message = "Prescription Created Successfully", drugs });
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
        [Route("Dispensing/id")]
        public async Task<IActionResult> Dispensing(int id)
        {
            /*if (!ModelState.IsValid)
                return BadRequest(new { State = ModelState, PrescriptionId = prescriptionId });
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();
            var prescription = await _unitOfWork.Prescription.GetFirstOrDefaultAsync(p => p.Id == prescriptionId);
            if (prescription == null)
                return NotFound();*/

            var prescriptionDetails = await _unitOfWork.PrescriptionDetails.GetAllFilterAsync(p => p.PrescriptionId == id);
            var orderDetailsDto = _unitOfWork.PrescriptionDetails.PrescriptionDetailsToOrderDetails(prescriptionDetails.ToList());
            /*  using (var client = new HttpClient())
              {
                  var url = "https://localhost:44332/Order/Checkout"; // replace with the actual API endpoint

                  // create an object to hold the data you want to submit
                  var data = orderDetailsdto;

                  // convert the data to a JSON string
                  var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                  // create a StringContent object from the JSON string
                  var content = new StringContent(json, Encoding.UTF8, "application/json");

                  // send the POST request and wait for the response
                  var response = await client.PostAsync(url, content);

                  // read the response content as a string
                  var responseString = await response.Content.ReadAsStringAsync();

                  // print the response to the console
                  Console.WriteLine(responseString);
              }*/
            var orderDetails = _mapper.Map<IEnumerable<OrderDetail>>(orderDetailsDto).ToList();
            var order = new Order { UserId = "2357bf53-13ec-4199-bd9a-54331c86622e" /*user.Id*/ };
            await _unitOfWork.Order.AddAsync(order);
            await _unitOfWork.SaveAsync();
            order.OrderTotal = _unitOfWork.Order.GetTotalPrice(orderDetails);
            await _unitOfWork.OrderDetail.SetOrderId(order.Id, orderDetails);
            var session = await _unitOfWork.Order.StripeSetting(order, orderDetails);
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
            /*
                        var httpClient = new HttpClient();
                        var drugs = orderDetailsDto; // populate with order details
                        var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(drugs);
                        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        var response = await httpClient.PostAsync("https://localhost:44332/Order/Checkout", httpContent);

                        if (response.IsSuccessStatusCode)
                        {
                            return Ok(response);
                        }
                        else
                        {
                            return BadRequest(response);
                        }*/
        }
    }
}