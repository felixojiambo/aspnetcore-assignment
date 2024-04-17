using Microsoft.AspNetCore.Mvc;
using Queue_Management_System.Interfaces;
using Queue_Management_System.Models;
using System.Diagnostics;

namespace Queue_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IServicePointService _servicePointService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IServicePointService servicePointService)
        {
            _logger = logger;
            _userService = userService;
            _servicePointService = servicePointService;
        }

        public async Task<IActionResult> Index()
        {
            // Example: Fetch all users and pass them to the view
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> ServicePoints()
        {
            // Example: Fetch all service points and pass them to the view
            var servicePoints = await _servicePointService.GetAllServicePointsAsync();
            return View(servicePoints);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
