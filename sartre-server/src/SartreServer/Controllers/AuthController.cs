using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("password"), Authorize]
        public IActionResult ChangePassword([FromBody] PasswordChangeRequest passwordChangeRequest)
        {
            if (passwordChangeRequest == null ||
                string.IsNullOrWhiteSpace(passwordChangeRequest.PreviousPassword) ||
                string.IsNullOrWhiteSpace(passwordChangeRequest.NewPassword) ||
                string.IsNullOrWhiteSpace(passwordChangeRequest.NewPasswordRepetition))
            {
                return HandleBadRequest("The previous user password needs to be supplied additionaly to a duplicate new password.");
            }

            if (passwordChangeRequest.NewPassword != passwordChangeRequest.NewPasswordRepetition)
            {
                return HandleBadRequest("Submitted passwords don't match!.");
            }

            try
            {
                string login = GetSubjectName();
                AuthService.ChangePassword(login, passwordChangeRequest.PreviousPassword, passwordChangeRequest.NewPassword);
                return Ok();
            }
            catch (UserNotFoundException exception)
            {
                return HandleUnexpectedException(exception);
            }
            catch (UnauthorizedAccessException)
            {
                return HandleBadRequest("The submitted password is wrong.");
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }
    }
}
