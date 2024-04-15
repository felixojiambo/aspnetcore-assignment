using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using Queue_Management_System.Models;

namespace Queue_Management_System.Data
{
    public class ServicePointRepository
    {
        private readonly string _connectionString;

        public ServicePointRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("QueueManagementSystem");
        }

        public void CreateServicePoint(ServicePoint servicePoint)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO service_points (name, provider_id) VALUES (@name, @providerId)", connection))
                {
                    command.Parameters.AddWithValue("name", servicePoint.Name);
                    command.Parameters.AddWithValue("providerId", servicePoint.ProviderId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<ServicePoint> GetServicePoints()
        {
            var servicePoints = new List<ServicePoint>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM service_points", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            servicePoints.Add(new ServicePoint
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ProviderId = reader.GetInt32(2)
                            });
                        }
                    }
                }
            }
            return servicePoints;
        }

        public void UpdateServicePoint(ServicePoint servicePoint)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE service_points SET name = @name, provider_id = @providerId WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", servicePoint.Id);
                    command.Parameters.AddWithValue("name", servicePoint.Name);
                    command.Parameters.AddWithValue("providerId", servicePoint.ProviderId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteServicePoint(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("DELETE FROM service_points WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
