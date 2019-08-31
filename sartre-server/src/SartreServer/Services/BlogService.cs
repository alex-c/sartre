using Microsoft.Extensions.Logging;
using SartreServer.Models;
using SartreServer.Repositories;
using SartreServer.Services.Exceptions;
using System.Collections.Generic;

namespace SartreServer.Services
{
    public class BlogService
    {
        private ILogger Logger { get; }

        private IBlogRepository BlogRepository { get; }

        public BlogService(ILoggerFactory loggerFactroy, IBlogRepository blogRepository)
        {
            Logger = loggerFactroy.CreateLogger<UserService>();
            BlogRepository = blogRepository;
        }

        public IEnumerable<Blog> GetAllBlogs()
        {
            return BlogRepository.GetAllBlogs();
        }

        public Blog GetBlog(string blogId)
        {
            Blog blog = BlogRepository.GetBlog(blogId);
            if (blog == null)
            {
                throw new BlogNotFoundException(blogId);
            }
            else
            {
                return blog;
            }
        }
    }
}
