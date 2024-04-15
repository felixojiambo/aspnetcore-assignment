using System.Collections.Generic;
using Npgsql;
using Queue_Management_System.Models;
using Queue_Management_System.Data;

namespace Queue_Management_System.Data
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly string _connectionString;

        public ServiceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Service> GetAllServices()
        {
            var services = new List<Service>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM services", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            services.Add(new Service
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                ServiceTypeId = reader.GetInt32(reader.GetOrdinal("service_type_id"))
                            });
                        }
                    }
                }
            }
            return services;
        }

        public Service GetServiceById(int id)
        {
            Service service = null;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM services WHERE id = @Id", connection))
                {
                    command.Parameters.AddWithValue("Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            service = new Service
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                ServiceTypeId = reader.GetInt32(reader.GetOrdinal("service_type_id"))
                            };
                        }
                    }
                }
            }
            return service;
        }

        public void AddService(Service service)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO services (name, service_type_id) VALUES (@Name, @ServiceTypeId) RETURNING id", connection))
                {
                    command.Parameters.AddWithValue("Name", service.Name);
                    command.Parameters.AddWithValue("ServiceTypeId", service.ServiceTypeId);
                    var id = (int)command.ExecuteScalar();
                    service.Id = id;
                }
            }
        }

        public void UpdateService(Service service)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE services SET name = @Name, service_type_id = @ServiceTypeId WHERE id = @Id", connection))
                {
                    command.Parameters.AddWithValue("Name", service.Name);
                    command.Parameters.AddWithValue("ServiceTypeId", service.ServiceTypeId);
                    command.Parameters.AddWithValue("Id", service.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteService(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("DELETE FROM services WHERE id = @Id", connection))
                {
                    command.Parameters.AddWithValue("Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
