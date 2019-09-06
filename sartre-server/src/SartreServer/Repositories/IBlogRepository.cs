using SartreServer.Models;
using System.Collections.Generic;

namespace SartreServer.Repositories
{
    /// <summary>
    /// A blog repository allows to get/create/update/delete blogs.
    /// </summary>
    public interface IBlogRepository
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

        /// <summary>
        /// Creates a new blog.
        /// </summary>
        /// <param name="blog">The blog to create.</param>
        void CreateBlog(Blog blog);

        /// <summary>
        /// Updates a blog.
        /// </summary>
        /// <param name="blog">The blog top update.</param>
        void UpdateBlog(Blog blog);

        /// <summary>
        /// Deletes a blog, identified by its ID.
        /// </summary>
        /// <param name="blogId">The ID of the blog to delete.</param>
        void DeleteBlog(string blogId);
    }
}
