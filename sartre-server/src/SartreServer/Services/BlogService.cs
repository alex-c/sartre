using Microsoft.Extensions.Logging;
using SartreServer.Models;
using SartreServer.Repositories;
using SartreServer.Services.Exceptions;
using System;
using System.Collections.Generic;

namespace SartreServer.Services
{
    public class BlogService
    {
        private ILogger Logger { get; }

        private IBlogRepository BlogRepository { get; }

        private IPostRepository PostRepository { get; }

        public BlogService(ILoggerFactory loggerFactroy, IBlogRepository blogRepository, IPostRepository postRepository)
        {
            Logger = loggerFactroy.CreateLogger<UserService>();
            BlogRepository = blogRepository;
            PostRepository = postRepository;
        }

        public IEnumerable<Blog> GetAllBlogs()
        {
            return BlogRepository.GetAllBlogs();
        }

        public IEnumerable<Post> GetBlogPosts(string blogId, int page, int itemsPerPage)
        {
            return PostRepository.GetPostsOfBlog(blogId, page, itemsPerPage);
        }

        public IEnumerable<User> GetBlogContributors(string blogId)
        {
            throw new NotImplementedException(); // TODO: implement this
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
