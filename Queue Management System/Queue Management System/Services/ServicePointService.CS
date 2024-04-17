using Queue_Management_System.Models;

namespace Queue_Management_System.Services
{
    public class ServicePointService : IServicePointService
    {
        private readonly ApplicationDbContext _context;

        public ServicePointService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServicePoint>> GetAllServicePointsAsync()
        {
            return await _context.ServicePoints.ToListAsync();
        }

        public async Task<ServicePoint> GetServicePointByIdAsync(int id)
        {
            return await _context.ServicePoints.FindAsync(id);
        }

        public async Task CreateServicePointAsync(ServicePoint servicePoint)
        {
            _context.ServicePoints.Add(servicePoint);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateServicePointAsync(ServicePoint servicePoint)
        {
            _context.ServicePoints.Update(servicePoint);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteServicePointAsync(int id)
        {
            var servicePoint = await _context.ServicePoints.FindAsync(id);
            if (servicePoint != null)
            {
                _context.ServicePoints.Remove(servicePoint);
                await _context.SaveChangesAsync();
            }
        }
    }
}
