using System.Collections.Generic;

namespace SartreServer.Contracts.Requests
{
    /// <summary>
    /// A request to update a user's roles.
    /// </summary>
    public class UpdateUserRoleRequest
    {
        /// <summary>
        /// The user's unique login name.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// The user's role IDs.
        /// </summary>
        public IEnumerable<int> RoleIds { get; set; }
    }
}
