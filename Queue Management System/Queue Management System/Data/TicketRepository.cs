using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using Queue_Management_System.Models;

namespace Queue_Management_System.Data
{
    public class TicketRepository
    {
        private readonly string _connectionString;

        public TicketRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("QueueManagementSystem");
        }

        public void CreateTicket(Ticket ticket)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO tickets (customer_id, service_point_id, status, created_at, updated_at) VALUES (@customerId, @servicePointId, @status, @createdAt, @updatedAt)", connection))
                {
                    command.Parameters.AddWithValue("customerId", ticket.CustomerId);
                    command.Parameters.AddWithValue("servicePointId", ticket.ServicePointId);
                    command.Parameters.AddWithValue("status", ticket.Status);
                    command.Parameters.AddWithValue("createdAt", ticket.CreatedAt);
                    command.Parameters.AddWithValue("updatedAt", ticket.UpdatedAt);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Ticket> GetTickets()
        {
            var tickets = new List<Ticket>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM tickets", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tickets.Add(new Ticket
                            {
                                Id = reader.GetInt32(0),
                                CustomerId = reader.GetInt32(1),
                                ServicePointId = reader.GetInt32(2),
                                Status = reader.GetString(3),
                                CreatedAt = reader.GetDateTime(4),
                                UpdatedAt = reader.GetDateTime(5)
                            });
                        }
                    }
                }
            }
            return tickets;
        }

        public void UpdateTicket(Ticket ticket)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE tickets SET customer_id = @customerId, service_point_id = @servicePointId, status = @status, updated_at = @updatedAt WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", ticket.Id);
                    command.Parameters.AddWithValue("customerId", ticket.CustomerId);
                    command.Parameters.AddWithValue("servicePointId", ticket.ServicePointId);
                    command.Parameters.AddWithValue("status", ticket.Status);
                    command.Parameters.AddWithValue("updatedAt", ticket.UpdatedAt);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTicket(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("DELETE FROM tickets WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
