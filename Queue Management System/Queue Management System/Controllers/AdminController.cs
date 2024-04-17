using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queue_Management_System.Interfaces;
using Queue_Management_System.Models;
using ServicePoint = Queue_Management_System.Models.ServicePoint; // Ensure this line is added

namespace Queue_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IServicePointService _servicePointService;

        public AdminController(IUserService userService, IServicePointService servicePointService)
        {
            _userService = userService;
            _servicePointService = servicePointService;
        }

        // GET: api/Admin/Users
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/Admin/ServicePoints
        [HttpGet("ServicePoints")]
        public async Task<ActionResult<IEnumerable<ServicePoint>>> GetServicePoints()
        {
            var servicePoints = await _servicePointService.GetAllServicePointsAsync();
            return Ok(servicePoints);
        }

        // POST: api/Admin/Users
        [HttpPost("Users")]
        public async Task<ActionResult<ApplicationUser>> CreateUser(ApplicationUser user)
        {
            await _userService.CreateUserAsync(user);
            return CreatedAtAction("GetUsers", new { id = user.Id }, user);
        }

        // PUT: api/Admin/Users/{id}
       [HttpPut("Users/{id}")]
public async Task<IActionResult> UpdateUser(int id, ApplicationUser user)
{
    // Convert id to string before comparing
    if (id.ToString() != user.Id)
    {
        return BadRequest();
    }

    await _userService.UpdateUserAsync(user);
    return NoContent();
}

        // DELETE: api/Admin/Users/{id}
        [HttpDelete("Users/{id}")]
public async Task<IActionResult> DeleteUser(int id)
{
    // Convert id to string before passing it to DeleteUserAsync
    await _userService.DeleteUserAsync(id.ToString());
    return NoContent();
}

        // Additional actions for service points can be defined similarly
 
    
    // [Authorize]
    // public class AdminController : Controller
    // {
    //     private readonly ServicePointRepository _servicePointRepository;
    //     private readonly ServiceProviderRepository _serviceProviderRepository;

    //     public AdminController(ServicePointRepository servicePointRepository, ServiceProviderRepository serviceProviderRepository)
    //     {
    //         _servicePointRepository = servicePointRepository;
    //         _serviceProviderRepository = serviceProviderRepository;
    //     }

    //     [HttpGet]
    //     public IActionResult Dashboard()
    //     {
    //         return View();
    //     }

    //     [HttpGet]
    //     public IActionResult ConfigureServicePoints()
    //     {
    //         var servicePoints = _servicePointRepository.GetServicePoints();
    //         return View(servicePoints);
    //     }

    //     [HttpGet]
    //     public IActionResult ConfigureProviders()
    //     {
    //         var providers = _serviceProviderRepository.GetServiceProviders();
    //         return View(providers);
    //     }

    //     [HttpGet]
    //     public IActionResult GenerateReport()
    //     {
    //         return View();
    //     }

    //     [HttpPost]
    //     public IActionResult GenerateReport(string reportType)
    //     {
    //         Report report = new Report();
    //         report.Load("path/to/your/report/template.frx");
          
    //         string exportPath = Path.Combine("path/to/exported/reports", $"{reportType}.pdf");
    //         report.Export(new FastReport.Export.Pdf.PDFExport(), exportPath);
    //         return File(exportPath, "application/pdf", $"{reportType}.pdf");
    //     }
    // }
}
}