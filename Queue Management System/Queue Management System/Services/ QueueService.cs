namespace Queue_Management_System.Services
{
    public class  QueueService
    {
         private readonly IHubContext<QueueHub> _hubContext;

    public QueueService(IHubContext<QueueHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task AddTicketToQueue(string ticketNumber, string servicePoint)
    {
        // Logic to add the ticket to the queue

        // Trigger an update to all connected clients
        await _hubContext.Clients.All.SendAsync("ReceiveTicketUpdate", ticketNumber, servicePoint);
    }
    }
}