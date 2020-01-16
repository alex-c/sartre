using SartreServer.Models;
using System.Collections.Generic;

namespace SartreServer.Repositories
{
    /// <summary>
    /// A blog repository allows read-only access to blog information.
    /// </summary>
    public interface IReadOnlyBlogRepository
    {
        /// <summary>
        /// Gets all blogs.
        /// </summary>
        /// <returns>Returns all blogs.</returns>
        IEnumerable<Blog> GetAllBlogs();

        /// <summary>
        /// Gets a blog by its ID.
        /// </summary>
        /// <param name="blogId">ID of the blog.</param>
        /// <returns>Returns the blog, or null if no matching blog was found.</returns>
        Blog GetBlog(string blogId);
    }
}
