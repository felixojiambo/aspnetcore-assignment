using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using Queue_Management_System.Models;
using ServiceProvider = Queue_Management_System.Models.ServiceProvider;

namespace Queue_Management_System.Data
{
    public class ServiceProviderRepository
    {
        private readonly string _connectionString;

        public ServiceProviderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("QueueManagementSystem");
        }

        public void CreateServiceProvider(ServiceProvider serviceProvider)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO service_providers (name) VALUES (@name)", connection))
                {
                    command.Parameters.AddWithValue("name", serviceProvider.Name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<ServiceProvider> GetServiceProviders()
        {
            var serviceProviders = new List<ServiceProvider>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM service_providers", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            serviceProviders.Add(new ServiceProvider
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            return serviceProviders;
        }

        public void UpdateServiceProvider(ServiceProvider serviceProvider)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE service_providers SET name = @name WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", serviceProvider.Id);
                    command.Parameters.AddWithValue("name", serviceProvider.Name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteServiceProvider(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("DELETE FROM service_providers WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
