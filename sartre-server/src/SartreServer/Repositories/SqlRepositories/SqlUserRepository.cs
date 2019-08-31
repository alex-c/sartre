using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SartreServer.Models;
using System.Collections.Generic;
using System.Data;

namespace SartreServer.Repositories.SqlRepositories
{
    /// <summary>
    /// A user repository based on a PostrgreSQL database.
    /// </summary>
    public class SqlUserRepository : IUserRepository
    {
        /// <summary>
        /// Connection string for the underlying database.
        /// </summary>
        private string ConnectionString { get; }

        /// <summary>
        /// Sets up a PostgreSQL-based user repository from the app configuration.
        /// </summary>
        /// <param name="configuration">Application configuration.</param>
        public SqlUserRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("Database:ConnectionString");
        }

        /// <summary>
        /// Gets a new PostgreSQL connection.
        /// </summary>
        /// <returns>Returns a new connection.</returns>
        internal IDbConnection GetNewConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>Returns all users.</returns>
        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> users = null;
            using (IDbConnection connection = GetNewConnection())
            {
                users = connection.Query<User>("SELECT * FROM users");
            }
            return users;
        }

        /// <summary>
        /// Gets a user by his login name.
        /// </summary>
        /// <param name="login">Login name of the user.</param>
        /// <returns>Returns the user, or null if no matching user was found.</returns>
        public User GetUser(string login)
        {
            User user = null;
            using (IDbConnection connection = GetNewConnection())
            {
                user = connection.QueryFirst<User>("SELECT * FROM users WHERE login=@Login", new { Login = login });
            }
            return user;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        public void CreateUser(User user)
        {
            using (IDbConnection connection = GetNewConnection())
            {
                connection.Execute("INSERT INTO users (login, name, password) VALUES (@Login, @Name, @Password)", new {
                    user.Login,
                    user.Name,
                    user.Password
                });
            }
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="user">The user top update.</param>
        public void UpdateUser(User user)
        {
            using (IDbConnection connection = GetNewConnection())
            {
                connection.Execute("UPDATE users SET name=@Name, password=@Password, biography=@Biography, website=@Website WHERE login = @Login", new
                {
                    user.Name,
                    user.Password,
                    user.Biography,
                    user.Website,
                    user.Login
                });
            }
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        public void DeleteUser(User user)
        {
            using (IDbConnection connection = GetNewConnection())
            {
                connection.Execute("DELETE FROM users WHERE login = @Login", new { user.Login });
            }
        }
    }
}
