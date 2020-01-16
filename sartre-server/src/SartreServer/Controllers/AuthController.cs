using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SartreServer.Contracts;
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
            if (loginRequest == null)
            {
                return BadRequest();
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
