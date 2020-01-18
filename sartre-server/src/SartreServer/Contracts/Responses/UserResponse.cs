using SartreServer.Models;
using System.Collections.Generic;

namespace SartreServer.Contracts.Responses
{
    /// <summary>
    /// A generic user contract for responses containing user data.
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Creates an instance from a user model.
        /// </summary>
        /// <param name="user">The user model for which to create an instance.</param>
        public UserResponse(User user)
        {
            Login = user.Login;
            Name = user.Name;
            Biography = user.Biography;
            Website = user.Website;
            Roles = new Dictionary<int, string>();
            foreach (Role role in user.Roles)
            {
                Roles.Add((int)role, role.ToString());
            }
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
        public IDictionary<int, string> Roles { get; set; }
    }
}
