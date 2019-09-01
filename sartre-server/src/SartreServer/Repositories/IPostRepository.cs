using SartreServer.Models;
using System.Collections.Generic;

namespace SartreServer.Repositories
{
    /// <summary>
    /// A post repository allows to get/create/update/delete posts.
    /// </summary>
    public interface IPostRepository
    {
        /// <summary>
        /// Gets all posts.
        /// </summary>
        /// <returns>Returns all posts.</returns>
        IEnumerable<Post> GetAllPosts();

        /// <summary>
        /// Gets paginated posts for a given blog.
        /// </summary>
        /// <param name="blogId">Id of the blog to get posts for.</param>
        /// <param name="page">Page of the post list.</param>
        /// <param name="itemsPerPage">Number of items per page for the post list.</param>
        /// <returns>Returns the paginated posts.</returns>
        IEnumerable<Post> GetPostsOfBlog(string blogId, int page, int itemsPerPage);

        /// <summary>
        /// Gets a post by its ID.
        /// </summary>
        /// <param name="postId">ID of the post.</param>
        /// <returns>Returns the post, or null if no matching post was found.</returns>
        Post GetPost(string postId);

        /// <summary>
        /// Creates a new post with the post creator as author.
        /// </summary>
        /// <param name="post">The post to create.</param>
        /// <param name="login">Login name of the post author.</param>
        void CreatePost(Post post, string login);

        /// <summary>
        /// Updates a post.
        /// </summary>
        /// <param name="post">The post to update.</param>
        void UpdatePost(Post post);

        /// <summary>
        /// Deletes a post, identified by its ID.
        /// </summary>
        /// <param name="postId">The ID of the post to delete.</param>
        void DeletePost(string postId);
    }
}
