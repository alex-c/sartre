namespace SartreServer.Contracts.Responses
{
    /// <summary>
    /// Holds a type of home page, which can be either a single blog-view, or a list of available blogs.
    /// </summary>
    public enum HomePageType
    {
        /// <summary>
        /// The home page is of the single blog type.
        /// </summary>
        Blog,

        /// <summary>
        /// The home page is of the blog list type.
        /// </summary>
        BlogList
    }
}
