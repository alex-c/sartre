using System;

namespace SartreServer.Services.Exceptions
{
    public class UserNotFoundException : Exception, IResourceNotFoundException
    {
        public UserNotFoundException() : base("User could not be found.") { }

        public UserNotFoundException(string user) : base($"User `{user}` could not be found.") { }
    }
}
