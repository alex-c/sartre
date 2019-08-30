using SartreServer.Repositories;

namespace SartreServer.Services
{
    public class UserService
    {
        private IUserRepository UserRepository { get; }

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
    }
}
