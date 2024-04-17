using Queue_Management_System.Models;

namespace Queue_Management_System.Interfaces
{
    public interface IServicePointService
    {
        Task<IEnumerable<ServicePoint>> GetAllServicePointsAsync();
        
    }
}