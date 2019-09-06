using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SartreServer.Models;
using System.Collections.Generic;
using System.Data;

namespace SartreServer.Repositories.SqlRepositories
{
    /// <summary>
    /// A blog repository based on a PostrgreSQL database.
    /// </summary>
    public class SqlBlogRepository : IBlogRepository
    {
        /// <summary>
        /// Connection string for the underlying database.
        /// </summary>
        private string ConnectionString { get; }

        /// <summary>
        /// Sets up a PostgreSQL-based blog repository from the app configuration.
        /// </summary>
        /// <param name="configuration">Application configuration.</param>
        public SqlBlogRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("Database:ConnectionString");
        }

        /// <summary>
        /// Gets a new PostgreSQL connection.
        /// </summary>
        /// <returns>Returns a new connection.</returns>
        internal IDbConnection GetNewConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }

        /// <summary>
        /// Gets all blogs.
        /// </summary>
        /// <returns>Returns all blogs.</returns>
        public IEnumerable<Blog> GetAllBlogs()
        {
            IEnumerable<Blog> blogs = null;
            using (IDbConnection connection = GetNewConnection())
            {
                blogs = connection.Query<Blog>("SELECT * FROM blogs");
            }
            return blogs;
        }

        /// <summary>
        /// Gets a blog by its ID.
        /// </summary>
        /// <param name="blogId">ID of the blog.</param>
        /// <returns>Returns the blog, or null if no matching blog was found.</returns>
        public Blog GetBlog(string blogId)
        {
            Blog blog = null;
            using (IDbConnection connection = GetNewConnection())
            {
                blog = connection.QueryFirst<Blog>("SELECT * FROM blogs WHERE id=@BlogId", new { BlogId = blogId });
            }
            return blog;
        }

        /// <summary>
        /// Creates a new blog.
        /// </summary>
        /// <param name="blog">The blog to create.</param>
        public void CreateBlog(Blog blog)
        {
            using (IDbConnection connection = GetNewConnection())
            {
                connection.Execute("INSERT INTO blogs (id, title, description) VALUES (@Id, @Title, @Description)", new
                {
                    blog.Id,
                    blog.Title,
                    blog.Description
                });
            }
        }

        /// <summary>
        /// Updates a blog.
        /// </summary>
        /// <param name="blog">The blog top update.</param>
        public void UpdateBlog(Blog blog)
        {
            using (IDbConnection connection = GetNewConnection())
            {
                connection.Execute("UPDATE blogs SET title=@Title, description=@Description WHERE id=@Id", new
                {
                    blog.Title,
                    blog.Description,
                    blog.Id
                });
            }
        }

        /// <summary>
        /// Deletes a blog, identified by its ID.
        /// </summary>
        /// <param name="blogId">The ID of the blog to delete.</param>
        public void DeleteBlog(string blogId)
        {
            using (IDbConnection connection = GetNewConnection())
            {
                connection.Execute("DELETE FROM blogs WHERE id=@Id", new { Id = blogId });
            }
        }
    }
}
