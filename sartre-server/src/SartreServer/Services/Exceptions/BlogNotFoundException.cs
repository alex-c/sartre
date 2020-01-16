using System;

namespace SartreServer.Services.Exceptions
{
    public class BlogNotFoundException : Exception, INotFoundException
    {
        public BlogNotFoundException() : base("Blog could not be found.") { }

        public BlogNotFoundException(string blogId) : base($"Blog `{blogId}` could not be found.") { }
    }
}
