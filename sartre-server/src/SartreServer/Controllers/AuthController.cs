using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SartreServer.Contracts.Requests;
using SartreServer.Models;
using SartreServer.Services;
using SartreServer.Services.Exceptions;
using System;

namespace SartreServer.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private AuthService AuthService { get; }

        public AuthController(ILoggerFactory loggerFactory, AuthService authService)
        {
            Logger = loggerFactory.CreateLogger<AuthController>();
            AuthService = authService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || loginRequest.Login == null || loginRequest.Password == null)
            {
                return HandleBadRequest("A login name and password should be supplied for login requests.");
            }

            try
            {
                if (AuthService.TryAuthenticateUser(loginRequest.Login, loginRequest.Password, out User user))
                {
                    return Ok(AuthService.GenerateJsonWebToken(user));
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (UserNotFoundException)
            {
                return Unauthorized();
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }
    }
}
