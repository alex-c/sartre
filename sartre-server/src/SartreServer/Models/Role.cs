namespace SartreServer.Models
{
    /// <summary>
    /// Defines user roles on the Sartre platform.
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// A user with the administrator role may use the Sartre platform's administrative features.
        /// </summary>
        Administrator = 0,

        /// <summary>
        /// A user with the author role may write posts for blogs he is a contributor of.
        /// </summary>
        Author = 1
    }
}
