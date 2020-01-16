using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SartreServer.Models;
using SartreServer.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
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
        /// Grants access to user information.
        /// </summary>
        private IReadOnlyUserRepository UserRepository { get; }

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
        /// <param name="userRepository">User repository for access to user data.</param>
        /// <param name="configuration">App configuration for JWT signing information.</param>
        public AuthService(ILoggerFactory loggerFactroy, IReadOnlyUserRepository userRepository, IConfiguration configuration)
        {
            Logger = loggerFactroy.CreateLogger<AuthService>();
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
        /// <returns>Returns whether authentication was successful.</returns>
        public bool AuthenticateUser(string login, string password)
        {
            User user = UserRepository.GetUser(login);

            // TODO: add hashing
            return user.Password == password;
        }

        /// <summary>
        /// Generates a JWT for a given user.
        /// </summary>
        /// <param name="login">Login name of the user to generate a token for.</param>
        /// <returns>Returns the generated token.</returns>
        public string GenerateJsonWebToken(string login)
        {
            // TODO: add claims!
            JwtSecurityToken token = new JwtSecurityToken(JwtIssuer, null, null, expires: DateTime.Now.Add(JwtLifetime), signingCredentials: SigningCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
