using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SartreServer.Contracts.Requests;
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
                if (AuthService.AuthenticateUser(loginRequest.Login, loginRequest.Password))
                {
                    return Ok(AuthService.GenerateJsonWebToken(loginRequest.Login));
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
