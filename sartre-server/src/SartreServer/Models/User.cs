using System.Collections.Generic;

namespace SartreServer.Models
{
    /// <summary>
    /// A user of the Sartre platform.
    /// </summary>
    public class User
    {
        public User()
        {
            Roles = new List<Role>();
        }

        /// <summary>
        /// The user's unique login name.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// The user's name as it will be displayed in the UI.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user's hashed password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The salt used to hash this user's password.
        /// </summary>
        public byte[] Salt { get; set; }

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
        public ICollection<Role> Roles { get; set; }
    }
}
