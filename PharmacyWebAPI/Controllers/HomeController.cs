global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using PharmacyWebAPI.DataAccess.Repository.IRepository;
global using PharmacyWebAPI.Models;
global using System.Diagnostics;

namespace PharmacyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("PharmacyWebAPI");
        }
    }
}