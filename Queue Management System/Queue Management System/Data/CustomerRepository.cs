using Npgsql;
using Queue_Management_System.Models;

namespace Queue_Management_System.Data
{
    public class CustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("QueueManagementSystem");
        }

        public void CreateCustomer(string name, string serviceType)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO customers (name, service_type) VALUES (@name, @serviceType)", connection))
                {
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("serviceType", serviceType);
                    command.ExecuteNonQuery();
                }
            }
        }

      public List<Customer> GetCustomers()
{
    var customers = new List<Customer>();
    using (var connection = new NpgsqlConnection(_connectionString))
    {
        connection.Open();
        using (var command = new NpgsqlCommand("SELECT * FROM customers", connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        ServiceTypeId = reader.GetInt32(2),
                        // Leave ServiceType as null or populate it based on ServiceTypeId
                        ServiceType = null // or fetch ServiceType based on ServiceTypeId
                    });
                }
            }
        }
    }
    return customers;
}


        public void UpdateCustomer(int id, string name, string serviceType)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE customers SET name = @name, service_type = @serviceType WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("serviceType", serviceType);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCustomer(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("DELETE FROM customers WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        
        
    }
}
