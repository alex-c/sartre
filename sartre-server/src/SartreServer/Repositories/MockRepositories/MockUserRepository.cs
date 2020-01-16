using SartreServer.Models;
using System.Collections.Generic;

namespace SartreServer.Repositories.MockRepositories
{
    public class MockUserRepository : IUserRepository, IReadOnlyUserRepository
    {
        private Dictionary<string, User> Users { get; }

        public MockUserRepository(MockDataProvider dataProvider = null)
        {
            if (dataProvider == null)
            {
                Users = new Dictionary<string, User>();
            }
            else
            {
                Users = dataProvider.Users;
            }
        }

        public void CreateUser(User user)
        {
            Users.Add(user.Login, user);
        }

        public void DeleteUser(string login)
        {
            Users.Remove(login);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return Users.Values;
        }

        public User GetUser(string login)
        {
            return Users.GetValueOrDefault(login);
        }

        public void UpdateUser(User user)
        {
            Users[user.Login] = user;
        }
    }
}
