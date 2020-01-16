using System;

namespace SartreServer.Services.Exceptions
{
    public class UserAlreadyExistsException : Exception, IResourceAlreadyExsistsException
    {
        public UserAlreadyExistsException() : base("A user with that login name already exists.") { }

        public UserAlreadyExistsException(string login) : base($"A user with login name `{login}` already exists.") { }
    }
}
