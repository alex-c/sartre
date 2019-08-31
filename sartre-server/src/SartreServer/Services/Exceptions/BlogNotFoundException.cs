using System;

namespace SartreServer.Services.Exceptions
{
    public class BlogNotFoundException : Exception
    {
        public BlogNotFoundException() : base("Blog could not be found.") { }

        public BlogNotFoundException(string blog) : base($"Blog `{blog}` could not be found.") { }
    }
}
