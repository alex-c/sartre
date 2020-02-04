using Microsoft.Extensions.Logging;
using SartreServer.Models;
using SartreServer.Repositories;
using SartreServer.Services.Exceptions;
using System;
using System.Collections.Generic;

namespace SartreServer.Services
{
    /// <summary>
    /// Provides access to blog data and blog management.
    /// </summary>
    public class BlogService
    {
        /// <summary>
        /// A logger for local logging needs.
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// Provides access to blog data.
        /// </summary>
        private IBlogRepository BlogRepository { get; }

        /// <summary>
        /// Provides access to blog post data.
        /// </summary>
        private IPostRepository PostRepository { get; }

        /// <summary>
        /// Sets up the blog service.
        /// </summary>
        /// <param name="loggerFactroy">A factory to create loggers from.</param>
        /// <param name="blogRepository">Access to blog data.</param>
        /// <param name="postRepository">Access to blog post data.</param>
        public BlogService(ILoggerFactory loggerFactroy, IBlogRepository blogRepository, IPostRepository postRepository)
        {
            Logger = loggerFactroy.CreateLogger<UserService>();
            BlogRepository = blogRepository;
            PostRepository = postRepository;
        }

        /// <summary>
        /// Gets all available blogs.
        /// </summary>
        /// <returns>Returns a list of blogs.</returns>
        public IEnumerable<Blog> GetAllBlogs()
        {
            return BlogRepository.GetAllBlogs();
        }

        /// <summary>
        /// Gets a blog by it's ID.
        /// </summary>
        /// <param name="blogId">ID of the blog to get.</param>
        /// <returns>Returns the blog.</returns>
        /// <exception cref="BlogNotFoundException">Thrown if there is no blog with the provided ID.</exception>
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

        /// <summary>
        /// Gets a paginated portion of the posts of a blog.
        /// </summary>
        /// <param name="blogId">ID of the blog to get posts for.</param>
        /// <param name="page">Page of posts.</param>
        /// <param name="itemsPerPage">Items per page of posts.</param>
        /// <returns>Returns the paginated blog posts.</returns>
        public IEnumerable<Post> GetBlogPosts(string blogId, int page, int itemsPerPage)
        {
            return PostRepository.GetPostsOfBlog(blogId, page, itemsPerPage);
        }

        /// <summary>
        /// Gets the contributors of a blog.
        /// </summary>
        /// <param name="blogId">ID of the blog to get the contributors of.</param>
        /// <returns>Returns a list of users.</returns>
        public IEnumerable<User> GetBlogContributors(string blogId)
        {
            return BlogRepository.GetBlog(blogId).Contributors;
        }
    }
}
