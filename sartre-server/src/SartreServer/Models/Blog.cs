using System.Collections.Generic;

namespace SartreServer.Models
{
    /// <summary>
    /// A blog on the Sartre platform.
    /// </summary>
    public class Blog
    {
        /// <summary>
        /// The blog's ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The blog's title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A short description of the blog.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A list of contributors that are allowed to post to the blog.
        /// </summary>
        public IEnumerable<User> Contributors { get; set; }
    }
}
