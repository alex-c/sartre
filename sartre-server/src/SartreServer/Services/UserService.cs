using Microsoft.Extensions.Logging;
using SartreServer.Models;
using SartreServer.Repositories;
using SartreServer.Services.Exceptions;
using System.Collections.Generic;

namespace SartreServer.Services
{
    public class UserService
    {
        private ILogger Logger { get; }

        private IUserRepository UserRepository { get; }

        public UserService(ILoggerFactory loggerFactroy, IUserRepository userRepository)
        {
            Logger = loggerFactroy.CreateLogger<UserService>();
            UserRepository = userRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return UserRepository.GetAllUsers();
        }

        public User GetUser(string login)
        {
            User user = UserRepository.GetUser(login);
            if (user == null)
            {
                throw new UserNotFoundException(login);
            }
            else
            {
                return user;
            }
        }
    }
}
