using System.Collections.Generic;
using Queue_Management_System.Models;

namespace Queue_Management_System.Data
{
    public interface IServicePointRepository
    {
        void CreateServicePoint(ServicePoint servicePoint);
        List<ServicePoint> GetServicePoints();
        void UpdateServicePoint(ServicePoint servicePoint);
        void DeleteServicePoint(int id);
    }
}
