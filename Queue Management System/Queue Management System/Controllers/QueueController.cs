using System.Data;
using FastReport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queue_Management_System.Data;
using Queue_Management_System.Models;
using Queue_Management_System.ViewModels;

namespace Queue_Management_System.Controllers
{
    public class QueueController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ServicePointRepository _servicePointRepository;

        public QueueController(ICustomerRepository customerRepository, ServicePointRepository servicePointRepository, ITicketRepository ticketRepository)
        {
            _customerRepository = customerRepository;
            _ticketRepository = ticketRepository;
            _servicePointRepository = servicePointRepository;
        }
        [HttpGet]
        public IActionResult CheckinPage()
        {
            return View();
        }


[HttpPost]
public IActionResult CheckinPage(int serviceTypeId, string customerName)
{
    // Create a customer
    CreateCustomer(customerName, serviceTypeId);

    // Generate a ticket number
    int ticketNumber = GenerateTicketNumber();

    // Create a ticket for the customer
    CreateTicket(serviceTypeId);

    // Create a DataSet with the ticket data
    DataSet dataSet = new DataSet();
    DataTable dataTable = new DataTable("TicketData");
    dataTable.Columns.Add("TicketNumber", typeof(int));
    dataTable.Columns.Add("CustomerName", typeof(string));
    dataTable.Columns.Add("ServiceType", typeof(int));
    dataTable.Rows.Add(ticketNumber, customerName, serviceTypeId);
    dataSet.Tables.Add(dataTable);

    // Generate the ticket using FastReport.Net
    var report = new Report();
    report.Load("path/to/your/ticketTemplate.frx");
    report.RegisterData(dataSet, "TicketData");
    report.Prepare();

    // Save the report to a file or stream
    report.SavePrepared("path/to/save/ticket.frp");

    // Redirect to the waiting page with the ticket ID
    return RedirectToAction("WaitingPage", new { ticketId = ticketNumber });
}

        private void CreateTicket(int serviceTypeId)
        {
            throw new NotImplementedException();
        }

        private int GenerateTicketNumber()
        {
            throw new NotImplementedException();
        }

        private void CreateCustomer(string customerName, int serviceTypeId)
        {
            throw new NotImplementedException();
        }
[HttpGet]
public IActionResult WaitingPage(int ticketId)
{
    // Retrieve the ticket details using the ticket ID
    var ticket = _ticketRepository.GetTicketById(ticketId);
    if (ticket == null)
    {
        // Handle the case where the ticket does not exist
        return NotFound("Ticket not found.");
    }

    // Determine the service point based on the ticket or service type
    var servicePoint = _servicePointRepository.GetServicePointById(ticket.ServicePointId);
    string servicePointName = servicePoint?.Name ?? "Unknown";

    // Prepare the WaitingModel with the ticket number and service point
    var model = new WaitingModel
    {
        TicketNumber = ticket.Id.ToString(), // Assuming the ID is the ticket number
        ServicePoint = servicePointName
    };

    // Return the WaitingPage view with the model
    return View(model);
}

        [Authorize, HttpGet]
public IActionResult ServicePoint(int servicePointId)
{
    // Retrieve the next ticket in the queue for the specified service point
    var nextTicket = _ticketRepository.GetNextTicketInQueue(servicePointId);
    if (nextTicket != null)
    {
        // Pass the next ticket to the view
        ViewBag.NextTicket = nextTicket;
        return View();
    }
    else
    {
        // Handle the case where there are no tickets in the queue
        ViewBag.Message = "No tickets are currently in the queue.";
        return View("NoTicketsView"); // Assuming you have a view for this case
    }
}

        // [Authorize, HttpPost]
        // public IActionResult RecallNumber(int ticketId)
        // {
        //     // Retrieve the ticket by its ID
        //     var ticket = _ticketRepository.GetTicketById(ticketId);
        //     if (ticket != null)
        //     {
        //         // Update the ticket status to "Recalled"
        //         ticket.Status = "Recalled";
        //         ticket.UpdatedAt = DateTime.Now;
        //         _ticketRepository.UpdateTicket(ticket);
        //         return RedirectToAction("ServicePoint", new { servicePointId = ticket.ServicePointId });
        //     }
        //     else
        //     {
        //         // Handle the case where the ticket does not exist
        //         return RedirectToAction("ServicePoint", new { servicePointId = 1 }); // Redirect to a default service point or handle accordingly
        //     }
        // }

        [HttpGet]
        public IActionResult SelectServicePoint()
        {
            var servicePoints = _servicePointRepository.GetServicePoints();
            return View(servicePoints);
        }

        [HttpPost]
        public IActionResult SelectServicePoint(int servicePointId)
        {
            // Store the selected service point ID in the session or TempData
            TempData["SelectedServicePointId"] = servicePointId;
            return RedirectToAction("TicketManagement");
        }


        // [HttpGet]
        // public IActionResult WaitingPage()
        // {
        //     return View();
        // }



        //     [Authorize, HttpGet]
        //     public IActionResult ServicePoint()
        //     {
        //         return View();
        //     }
[Authorize, HttpGet]
public IActionResult GetNextTicket(int servicePointId)
{
#pragma warning disable CS8605 // Unboxing a possibly null value.
            int selectedServicePointId = (int)TempData["SelectedServicePointId"];
#pragma warning restore CS8605 // Unboxing a possibly null value.
            var nextTicket = _servicePointRepository.GetNextTicket(selectedServicePointId);

    var viewModel = new ServicePointViewModel
    {
        SelectedServicePoint = _servicePointRepository.GetServicePointById(selectedServicePointId),
        Tickets = _ticketRepository.GetTicketsByServicePointId(selectedServicePointId),
        NextTicket = nextTicket
    };

    return View("ServicePoint", viewModel);
}

        [Authorize, HttpGet]
public IActionResult ServicePoint()
{
    int servicePointId = (int)TempData["SelectedServicePointId"];
    var servicePoint = _servicePointRepository.GetServicePointById(servicePointId);
    var tickets = _ticketRepository.GetTicketsByServicePointId(servicePointId);

    var viewModel = new ServicePointViewModel
    {
        SelectedServicePoint = servicePoint,
        Tickets = tickets
    };

    return View(viewModel);
}

[Authorize, HttpPost]
public IActionResult RecallNumber(int ticketId)
{
    _ticketRepository.RecallNumber(ticketId);
    return RedirectToAction("ServicePoint"); // Redirect to the ServicePoint view or another appropriate action
}

[Authorize, HttpPost]
public IActionResult MarkNoShow(int ticketId)
{
    _ticketRepository.MarkNoShow(ticketId);
    return RedirectToAction("ServicePoint"); // Redirect to the ServicePoint view or another appropriate action
}

[Authorize, HttpPost]
public IActionResult MarkFinished(int ticketId)
{
    _ticketRepository.MarkFinished(ticketId);
    return RedirectToAction("ServicePoint"); // Redirect to the ServicePoint view or another appropriate action
}

[Authorize, HttpPost]
public IActionResult TransferNumber(int ticketId, int newServicePointId)
{
    _ticketRepository.TransferNumber(ticketId, newServicePointId);
    return RedirectToAction("ServicePoint"); // Redirect to the ServicePoint view or another appropriate action
}






    }

}
