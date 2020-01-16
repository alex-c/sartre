using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SartreServer.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
            IEnumerable<User> users = null;
            using (IDbConnection connection = GetNewConnection())
            {
                users = connection.Query<User, Role, User>("SELECT * FROM users WHERE login=@Login JOIN user_roles ON users.login = user_roles.user_login JOIN roles ON roles.id = user_roles.role_id", (user, role) => {
                    user.Roles.Add(role);
                    return user;
                }, new { Login = login });
            }
            return users.FirstOrDefault();
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="login">The login name of the user to create.</param>
        /// <param name="name">The display name of the user to create.</param>
        /// <param name="password">The password of the user to create.</param>
        /// <returns>Returns the newly created used.</returns>
        public User CreateUser(string login, string name, string password)
        {
            IEnumerable<User> users = null;
            using (IDbConnection connection = GetNewConnection())
            {
                connection.Execute("INSERT INTO users (login, name, password) VALUES (@Login, @Name, @Password)", new {
                    login,
                    name,
                    password
                });
                users = connection.Query<User, Role, User>("SELECT * FROM users WHERE login=@Login JOIN user_roles ON users.login = user_roles.user_login JOIN roles ON roles.id = user_roles.role_id", (user, role) => {
                    user.Roles.Add(role);
                    return user;
                }, new { Login = login });
            }
            return users.FirstOrDefault();
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
        /// Deletes a user, identified by his login name.
        /// </summary>
        /// <param name="login">The login name of the user to delete.</param>
        public void DeleteUser(string login)
        {
            using (IDbConnection connection = GetNewConnection())
            {
                connection.Execute("DELETE FROM users WHERE login = @Login", new { Login = login });
            }
        }
    }
}
