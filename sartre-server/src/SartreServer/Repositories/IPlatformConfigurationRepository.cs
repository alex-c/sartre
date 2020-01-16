namespace SartreServer.Repositories
{
    /// <summary>
    /// A repository for the Sartre platform configuration.
    /// </summary>
    public interface IPlatformConfigurationRepository
    {
        /// <summary>
        /// Get the configured default blog ID. Can be null, if there is no default blog!
        /// </summary>
        /// <returns>Returns the default blog ID or null.</returns>
        string GetDefaultBlogId();

        /// <summary>
        /// Sets the default blog ID. Can be null!
        /// </summary>
        /// <param name="id">The ID of the blog to set as default blog or null.</param>
        void SetDefaultBlog(string id);
    }
}
