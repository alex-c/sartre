﻿using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SartreServer.Models;
using System.Collections.Generic;
using System.Data;

namespace SartreServer.Repositories.SqlRepositories
{
    /// <summary>
    /// A blog post repository based on a PostrgreSQL database.
    /// </summary>
    public class SqlPostRepository : IPostRepository
    {
        /// <summary>
        /// Connection string for the underlying database.
        /// </summary>
        private string ConnectionString { get; }

        /// <summary>
        /// Sets up a PostgreSQL-based blog post repository from the app configuration.
        /// </summary>
        /// <param name="configuration">Application configuration.</param>
        public SqlPostRepository(IConfiguration configuration)
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
        /// Gets all posts.
        /// </summary>
        /// <returns>Returns all posts.</returns>
        public IEnumerable<Post> GetAllPosts()
        {
            IEnumerable<Post> posts = null;
            using (IDbConnection connection = GetNewConnection())
            {
                posts = connection.Query<Post>("SELECT * FROM posts");
            }
            return posts;
        }

        /// <summary>
        /// Gets a post by its ID.
        /// </summary>
        /// <param name="postId">ID of the post.</param>
        /// <returns>Returns the post, or null if no matching post was found.</returns>
        public Post GetPost(string postId)
        {
            Post post = null;
            using (IDbConnection connection = GetNewConnection())
            {
                post = connection.QueryFirst<Post>("SELECT * FROM posts WHERE id=@PostId", new { PostId = postId });
            }
            return post;
        }

        /// <summary>
        /// Creates a new post with the post creator as author.
        /// </summary>
        /// <param name="post">The post to create.</param>
        /// <param name="login">Login name of the post author.</param>
        public void CreatePost(Post post, string login)
        {
            // TODO: move post_authors insert to a PostAuthors repository?
            using (IDbConnection connection = GetNewConnection())
            {
                connection.Execute("INSERT INTO posts (id, title, published, blog_id, content) VALUES (@Id, @Title, @Published, @BlogId, @Content)", new
                {
                    post.Id,
                    post.Title,
                    post.Published,
                    BlogId = post.Blog,
                    post.Content
                });
                connection.Execute("INSERT INTO post_authors (post_id, user_login) VALUES (@PostId, @UserLogin)", new
                {
                    PostId = post.Id,
                    UserLogin = login
                });
            }
        }

        /// <summary>
        /// Updates a post.
        /// </summary>
        /// <param name="post">The post top update.</param>
        public void UpdatePost(Post post)
        {
            using (IDbConnection connection = GetNewConnection())
            {
                connection.Execute("UPDATE posts SET title=@Title, published=@Published content=@Content WHERE id=@Id", new
                {
                    post.Title,
                    post.Published,
                    post.Content,
                    post.Id
                });
            }
        }

        /// <summary>
        /// Deletes a post, identified by its ID.
        /// </summary>
        /// <param name="postId">The ID of the post to delete.</param>
        public void DeletePost(string postId)
        {
            using (IDbConnection connection = GetNewConnection())
            {
                connection.Execute("DELETE FROM posts WHERE id=@Id", new { Id = postId });
            }
        }
    }
}
