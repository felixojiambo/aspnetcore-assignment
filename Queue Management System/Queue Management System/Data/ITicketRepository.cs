using Queue_Management_System.Models;

namespace Queue_Management_System.Data
{
    public interface ITicketRepository
    {
      void CreateTicket(Ticket ticket);
        List<Ticket> GetTickets();
        void UpdateTicket(Ticket ticket);
        void DeleteTicket(int id);
        Ticket GetNextTicketInQueue(int servicePointId);
        Ticket GetTicketById(int id);
        List<Ticket> GetTicketsByServicePointId(int servicePointId);
        void RecallNumber(int ticketId);
        void MarkNoShow(int ticketId);
        void MarkFinished(int ticketId);
        void TransferNumber(int ticketId, int newServicePointId);
    }
}
