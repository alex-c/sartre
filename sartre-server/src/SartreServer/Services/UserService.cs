﻿using Microsoft.Extensions.Logging;
using SartreServer.Models;
using SartreServer.Repositories;
using SartreServer.Services.Exceptions;
using System.Collections.Generic;

namespace SartreServer.Services
{
    /// <summary>
    /// Provides access to user data and user management.
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Logger for local logging.
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// Provides access to user data persistence.
        /// </summary>
        private IUserRepository UserRepository { get; }

        /// <summary>
        /// Sets up the service.
        /// </summary>
        /// <param name="loggerFactroy">Factory to create a logger from.</param>
        /// <param name="userRepository">Repository for user data.</param>
        public UserService(ILoggerFactory loggerFactroy, IUserRepository userRepository)
        {
            Logger = loggerFactroy.CreateLogger<UserService>();
            UserRepository = userRepository;
        }

        /// <summary>
        /// Gets all available users.
        /// </summary>
        /// <returns>Returns a list of users.</returns>
        public IEnumerable<User> GetAllUsers()
        {
            return UserRepository.GetAllUsers();
        }

        /// <summary>
        /// Gets a user by his unique login name.
        /// </summary>
        /// <param name="login">Login name of the user to get.</param>
        /// <returns>Returns the user if found.</returns>
        /// <exception cref="UserNotFoundException">Thrown if there is no user with the given login name.</exception>
        public User GetUser(string login)
        {
            User user = UserRepository.GetUser(login);
            if (user == null)
            {
                throw new UserNotFoundException(login);
            }
            return user;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="login">A login name for the user to create.</param>
        /// <param name="name">A display name for the user to create.</param>
        /// <param name="password">A password for the user.</param>
        /// <returns>Returns the newly created user.</returns>
        /// <exception cref="UserAlreadyExistsException">Thrown if the login name is already taken.</exception>
        public User CreateUser(string login, string name, string password)
        {
            if (UserRepository.GetUser(login) != null)
            {
                throw new UserAlreadyExistsException(login);
            }

            // TODO: add hashing!
            return UserRepository.CreateUser(login, name, password);
        }
    }
}
