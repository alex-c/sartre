namespace SartreServer.Contracts
{
    /// <summary>
    /// A request to set the Sartre platform's default blog.
    /// </summary>
    public class SetDefaultBlogRequest
    {
        /// <summary>
        /// The ID of the blog to set as the Sartre's default blog. Can be null.
        /// </summary>
        public string BlogId { get; set; }
    }
}
