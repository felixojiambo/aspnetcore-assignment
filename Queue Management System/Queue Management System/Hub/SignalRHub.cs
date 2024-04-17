using Microsoft.AspNetCore.SignalR;
namespace Queue_Management_System.Hub
{
    public class QueueHub : Hub
{
    public async Task SendTicketUpdate(string ticketNumber, string servicePoint)
    {
        await Clients.All.SendAsync("ReceiveTicketUpdate", ticketNumber, servicePoint);
    }
}}
