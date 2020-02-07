namespace SartreServer.Models
{
    /// <summary>
    /// The general Sartre platform configuration.
    /// </summary>
    public class PlatformConfiguration
    {
        /// <summary>
        /// The name of the blogging platform.
        /// </summary>
        public string PlatformName { get; set; }

        /// <summary>
        /// The platform's default blog's ID.
        /// </summary>
        public string DefaultBlogId { get; set; }
    }
}
