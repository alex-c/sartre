using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SartreServer.Models;
using SartreServer.Repositories;
using SartreServer.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SartreServer.Services
{
    /// <summary>
    /// Provides authentication tools.
    /// </summary>
    public class AuthService
    {
        /// <summary>
        /// Logger for local logging.
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// Provides password hashing functionalities.
        /// </summary>
        private PasswordHashingService PasswordHashingService { get; }

        /// <summary>
        /// Grants access to user information.
        /// </summary>
        private IUserRepository UserRepository { get; }

        /// <summary>
        /// Signing credentials for JWTs.
        /// </summary>
        private SigningCredentials SigningCredentials { get; }

        /// <summary>
        /// Lifetime of issued JWTs.
        /// </summary>
        private TimeSpan JwtLifetime { get; }

        /// <summary>
        /// Issuer name of issued JWTs.
        /// </summary>
        private string JwtIssuer { get; }

        /// <summary>
        /// Sets up the authentication service.
        /// </summary>
        /// <param name="loggerFactroy">Logger factory to create a local logger from.</param>
        /// <param name="passwordHashingService">Provides hashing functionality.</param>
        /// <param name="userRepository">User repository for access to user data.</param>
        /// <param name="configuration">App configuration for JWT signing information.</param>
        public AuthService(ILoggerFactory loggerFactroy, PasswordHashingService passwordHashingService, IUserRepository userRepository, IConfiguration configuration)
        {
            Logger = loggerFactroy.CreateLogger<AuthService>();
            PasswordHashingService = passwordHashingService;
            UserRepository = userRepository;
            
            // Generate signing credentials
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Secret")));
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtLifetime = TimeSpan.FromMinutes(configuration.GetValue<int>("Jwt:LifetimeInMinutes"));
            JwtIssuer = configuration.GetValue<string>("Jwt:Issuer");
        }

        /// <summary>
        /// Attempts to authenticate a user with a password and returns whether authentication was successful.
        /// </summary>
        /// <param name="login">Login name of the user to authenticate.</param>
        /// <param name="password">Password to attempt authentication with.</param>
        /// <param name="user">Contains the authenticated user if authentication is successful, else contains null.</param>
        /// <returns>Returns whether authentication was successful.</returns>
        /// <exception cref="UserNotFoundException">Thrown if no user with this login name could be found.</exception>
        public bool TryAuthenticateUser(string login, string password, out User user)
        {
            user = UserRepository.GetUser(login);
            if (user == null) {
                throw new UserNotFoundException(login);
            }
            return user.Password == PasswordHashingService.HashAndSaltPassword(password, user.Salt);
        }

        /// <summary>
        /// Generates a JWT for a given user.
        /// </summary>
        /// <param name="user">The user to generate a token for.</param>
        /// <returns>Returns the generated token.</returns>
        public string GenerateJsonWebToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                // Add subject
                new Claim(JwtRegisteredClaimNames.Sub, user.Login)
            };

            // Add role claims
            foreach (Role role in user.Roles)
            {
                claims.Add(new Claim("role", role.ToString()));
            }

            // Generate token
            JwtSecurityToken token = new JwtSecurityToken(JwtIssuer, null, claims, expires: DateTime.Now.Add(JwtLifetime), signingCredentials: SigningCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="login">The login name of the user to change the password for.</param>
        /// <param name="previousPassword">The user's previous password for verification purposes.</param>
        /// <param name="newPassword">The new password to hash and store.</param>
        /// <exception cref="UnauthorizedAccessException">Thrown if the submitted old password is wrong!</exception>
        public void ChangePassword(string login, string previousPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentException("No valid login name provided.", nameof(login));
            }

            if (string.IsNullOrWhiteSpace(previousPassword))
            {
                throw new ArgumentException("No valid old password provided.", nameof(previousPassword));
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException("No valid new password provided.", nameof(newPassword));
            }

            // Get user to change the password for
            User user = UserRepository.GetUser(login);
            if (user == null)
            {
                throw new UserNotFoundException(login);
            }

            // Verify old password
            if (user.Password != PasswordHashingService.HashAndSaltPassword(previousPassword, user.Salt))
            {
                throw new UnauthorizedAccessException();
            }

            // Hash and salt new password
            (string hashedPassword, byte[] salt) = PasswordHashingService.HashAndSaltPassword(newPassword);
            user.Password = hashedPassword;
            user.Salt = salt;
            UserRepository.UpdateUser(user);
        }
    }
}
