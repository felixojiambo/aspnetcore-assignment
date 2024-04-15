using System.Collections.Generic;
using Queue_Management_System.Models;
using ServiceProvider = Queue_Management_System.Models.ServiceProvider;

namespace Queue_Management_System.Data
{
    public interface IServiceProviderRepository
    {
        void CreateServiceProvider(ServiceProvider serviceProvider);
        List<ServiceProvider> GetServiceProviders();
        void UpdateServiceProvider(ServiceProvider serviceProvider);
        void DeleteServiceProvider(int id);
    }
}
