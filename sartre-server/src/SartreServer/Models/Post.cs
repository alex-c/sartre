using System.Collections.Generic;

namespace SartreServer.Models
{
    /// <summary>
    /// A blog post on the Sartre platform.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// The post's ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The blog this post is for.
        /// </summary>
        // TODO: Replace by just the blog ID?
        public Blog Blog { get; set; }

        /// <summary>
        /// The authors of ths post.
        /// </summary>
        public IEnumerable<User> Authors { get; set; }

        /// <summary>
        /// Whether the post has been published or is a draft.
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// The title of the post.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The content of the post.
        /// </summary>
        public string Content { get; set; }
    }
}
