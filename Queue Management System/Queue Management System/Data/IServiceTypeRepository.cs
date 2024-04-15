namespace Queue_Management_System.Models
{
    public interface IServiceTypeRepository
    {
         IEnumerable<ServiceType> GetAllServiceTypes();
        ServiceType GetServiceTypeById(int id);
        void AddServiceType(ServiceType serviceType);
        void UpdateServiceType(ServiceType serviceType);
        void DeleteServiceType(int id);
    }
}