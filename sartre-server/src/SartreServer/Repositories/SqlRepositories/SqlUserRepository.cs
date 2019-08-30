using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SartreServer.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SartreServer.Repositories.SqlRepositories
{
    public class SqlUserRepository : IUserRepository
    {
        private string ConnectionString { get; }

        public SqlUserRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("Database:ConnectionString");
        }

        internal IDbConnection GetNewConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }

        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> users = null;
            using (IDbConnection connection = GetNewConnection())
            {
                users = connection.Query<User>("SELECT * FROM users");
            }
            return users;
        }

        public User GetUser(string login)
        {
            User user = null;
            using (IDbConnection connection = GetNewConnection())
            {
                user = connection.QueryFirstOrDefault<User>("SELECT * FROM users WHERE login=@Login", new { Login = login });
            }
            return user;
        }

        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
