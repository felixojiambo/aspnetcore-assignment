using Queue_Management_System.Models;

namespace Queue_Management_System.Data
{
    public interface IServicePointRepository
    {    void CreateServicePoint(ServicePoint servicePoint);
    List<ServicePoint> GetServicePoints();
    void UpdateServicePoint(int id, ServicePoint servicePoint);
    void DeleteServicePoint(int id);
    ServicePoint GetServicePointById(int id);
    ServicePoint GetServicePointByName(string name);
    }
}
