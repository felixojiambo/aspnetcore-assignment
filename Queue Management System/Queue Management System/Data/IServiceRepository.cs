namespace Queue_Management_System.Models
{
    public interface IServiceRepository
    {
          IEnumerable<Service> GetAllServices();
        Service GetServiceById(int id);
        void AddService(Service service);
        void UpdateService(Service service);
        void DeleteService(int id);
    }
}