using Npgsql;
using Queue_Management_System.Models;

namespace Queue_Management_System.Data
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly string _connectionString;

        public ServiceTypeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ServiceType> GetAllServiceTypes()
        {
            var serviceTypes = new List<ServiceType>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM service_types", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            serviceTypes.Add(new ServiceType
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.GetString(reader.GetOrdinal("name"))
                            });
                        }
                    }
                }
            }
            return serviceTypes;
        }

        public ServiceType GetServiceTypeById(int id)
        {
            ServiceType serviceType = null;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM service_types WHERE id = @Id", connection))
                {
                    command.Parameters.AddWithValue("Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            serviceType = new ServiceType
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.GetString(reader.GetOrdinal("name"))
                            };
                        }
                    }
                }
            }
            return serviceType;
        }

        public void AddServiceType(ServiceType serviceType)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO service_types (name) VALUES (@Name) RETURNING id", connection))
                {
                    command.Parameters.AddWithValue("Name", serviceType.Name);
                    var id = (int)command.ExecuteScalar();
                    serviceType.Id = id;
                }
            }
        }

        public void UpdateServiceType(ServiceType serviceType)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE service_types SET name = @Name WHERE id = @Id", connection))
                {
                    command.Parameters.AddWithValue("Name", serviceType.Name);
                    command.Parameters.AddWithValue("Id", serviceType.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteServiceType(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("DELETE FROM service_types WHERE id = @Id", connection))
                {
                    command.Parameters.AddWithValue("Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
