using SartreServer.Models;
using System.Collections.Generic;

namespace SartreServer.Repositories
{
    /// <summary>
    /// A user repository allows to get/create/update/delete users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>Returns all users.</returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Gets a user by his login name.
        /// </summary>
        /// <param name="login">Login name of the user.</param>
        /// <returns>Returns the user, or null if no matching user was found.</returns>
        User GetUser(string login);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        void CreateUser(User user);

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="user">The user top update.</param>
        void UpdateUser(User user);

        /// <summary>
        /// Deletes a user, identified by his login name.
        /// </summary>
        /// <param name="login">The login name of the user to delete.</param>
        void DeleteUser(string login);
    }
}
