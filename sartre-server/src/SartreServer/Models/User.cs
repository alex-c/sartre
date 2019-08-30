using System.Collections.Generic;

namespace SartreServer.Models
{
    /// <summary>
    /// A user of the Sartre platform.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The user's unique login name.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// The user's name as it will be displayed in the UI.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A short user biography.
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        /// An URL to the user's website or an online profile.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// The platform roles the user has been assigned.
        /// </summary>
        public IEnumerable<Role> Roles { get; set; }
    }
}
