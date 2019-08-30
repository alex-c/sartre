using SartreServer.Models;
using System.Collections.Generic;

namespace SartreServer.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        User GetUser(string login);

        void CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);
    }
}
