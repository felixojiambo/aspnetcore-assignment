using Npgsql;
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
public Ticket GetNextTicket(int servicePointId)
{
    Ticket nextTicket = null;
    using (var connection = new NpgsqlConnection(_connectionString))
    {
        connection.Open();
        using (var command = new NpgsqlCommand("SELECT * FROM tickets WHERE service_point_id = @servicePointId AND status = 'pending' ORDER BY created_at ASC LIMIT 1", connection))
        {
            command.Parameters.AddWithValue("servicePointId", servicePointId);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    nextTicket = new Ticket
                    {
                        Id = reader.GetInt32(0),
                        CustomerId = reader.GetInt32(1),
                        ServicePointId = reader.GetInt32(2),
                        Status = reader.GetString(3),
                        CreatedAt = reader.GetDateTime(4),
                        UpdatedAt = reader.GetDateTime(5)
                    };
                }
            }
        }
    }
    return nextTicket;
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
        public ServicePoint GetServicePointById(int servicePointId)
{
    ServicePoint servicePoint = null;
    using (var connection = new NpgsqlConnection(_connectionString))
    {
        connection.Open();
        using (var command = new NpgsqlCommand("SELECT * FROM service_points WHERE id = @id", connection))
        {
            command.Parameters.AddWithValue("id", servicePointId);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    servicePoint = new ServicePoint
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        ProviderId = reader.GetInt32(2)
                    };
                }
            }
        }
    }
    return servicePoint;
}
public List<Ticket> GetTicketsByServicePointId(int servicePointId)
{
    List<Ticket> tickets = new List<Ticket>();
    using (var connection = new NpgsqlConnection(_connectionString))
    {
        connection.Open();
        using (var command = new NpgsqlCommand("SELECT * FROM tickets WHERE service_point_id = @servicePointId", connection))
        {
            command.Parameters.AddWithValue("servicePointId", servicePointId);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Ticket ticket = new Ticket
                    {
                        Id = reader.GetInt32(0),
                        CustomerId = reader.GetInt32(1),
                        ServicePointId = reader.GetInt32(2),
                        Status = reader.GetString(3),
                        CreatedAt = reader.GetDateTime(4),
                        UpdatedAt = reader.GetDateTime(5)
                    };
                    tickets.Add(ticket);
                }
            }
        }
    }
    return tickets;
}

    }
    
}
