using Npgsql;
using Queue_Management_System.Models;
namespace Queue_Management_System.Data
{
public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

   public ApplicationUser GetUserById(string id)
{
    using (var connection = new NpgsqlConnection(_connectionString))
    {
        connection.Open();
        using (var command = new NpgsqlCommand("SELECT * FROM users WHERE id = @id", connection))
        {
            command.Parameters.AddWithValue("id", id);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new ApplicationUser
                    {
                        Id = reader.GetString(0),
                        UserName = reader.GetString(1),
                        Email = reader.GetString(2),
                        PasswordHash = reader.GetString(3)
                        // Map other properties as necessary
                    };
                }
            }
        }
    }
    return null;
}


public void CreateUser(ApplicationUser user)
{
    using (var connection = new NpgsqlConnection(_connectionString))
    {
        connection.Open();
        using (var command = new NpgsqlCommand("INSERT INTO users (id, username, email, password_hash) VALUES (@id, @username, @email, @passwordHash)", connection))
        {
            command.Parameters.AddWithValue("id", user.Id);
            command.Parameters.AddWithValue("username", user.UserName);
            command.Parameters.AddWithValue("email", user.Email);
            command.Parameters.AddWithValue("passwordHash", user.PasswordHash);
            command.ExecuteNonQuery();
        }
    }
}
public void UpdateUser(ApplicationUser user)
{
    using (var connection = new NpgsqlConnection(_connectionString))
    {
        connection.Open();
        using (var command = new NpgsqlCommand("UPDATE users SET username = @username, email = @email, password_hash = @passwordHash WHERE id = @id", connection))
        {
            command.Parameters.AddWithValue("id", user.Id);
            command.Parameters.AddWithValue("username", user.UserName);
            command.Parameters.AddWithValue("email", user.Email);
            command.Parameters.AddWithValue("passwordHash", user.PasswordHash);
            command.ExecuteNonQuery();
        }
    }
}

    }}