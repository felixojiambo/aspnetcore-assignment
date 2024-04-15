using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FastReport;
using System.IO;
using Queue_Management_System.Data;

namespace Queue_Management_System.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ServicePointRepository _servicePointRepository;
        private readonly ServiceProviderRepository _serviceProviderRepository;

        public AdminController(ServicePointRepository servicePointRepository, ServiceProviderRepository serviceProviderRepository)
        {
            _servicePointRepository = servicePointRepository;
            _serviceProviderRepository = serviceProviderRepository;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ConfigureServicePoints()
        {
            var servicePoints = _servicePointRepository.GetServicePoints();
            return View(servicePoints);
        }

        [HttpGet]
        public IActionResult ConfigureProviders()
        {
            var providers = _serviceProviderRepository.GetServiceProviders();
            return View(providers);
        }

        [HttpGet]
        public IActionResult GenerateReport()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateReport(string reportType)
        {
            Report report = new Report();
            report.Load("path/to/your/report/template.frx");
          
            string exportPath = Path.Combine("path/to/exported/reports", $"{reportType}.pdf");
            report.Export(new FastReport.Export.Pdf.PDFExport(), exportPath);
            return File(exportPath, "application/pdf", $"{reportType}.pdf");
        }
    }
}
