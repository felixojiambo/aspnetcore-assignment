using Queue_Management_System.Models;

namespace Queue_Management_System.Data
{
    public interface ICustomerRepository
    {
        void CreateCustomer(string name, string serviceType);
        List<Customer> GetCustomers();
        void UpdateCustomer(int id, string name, string serviceType);
        void DeleteCustomer(int id);
    }
}
