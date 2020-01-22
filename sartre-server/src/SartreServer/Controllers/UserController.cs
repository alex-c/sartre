using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SartreServer.Contracts.Requests;
using SartreServer.Contracts.Responses;
using SartreServer.Models;
using SartreServer.Services;
using SartreServer.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SartreServer.Controllers
{
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private UserService UserService { get; }

        public UserController(ILoggerFactory loggerFactory, UserService userService)
        {
            Logger = loggerFactory.CreateLogger<UserController>();
            UserService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                IEnumerable<User> users = UserService.GetAllUsers();
                return Ok(users.Select(u => new UserResponse(u)));
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }

        [HttpGet("{login}")]
        public IActionResult GetUser(string login)
        {
            try
            {
                User user = UserService.GetUser(login);
                return Ok(new UserResponse(user));
            }
            catch (UserNotFoundException exception)
            {
                return HandleResourceNotFoundException(exception);
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }

        [HttpPost, Authorize]
        public IActionResult CreateUser([FromBody] UserCreationRequest userCreationRequest)
        {
            if (userCreationRequest == null ||
                string.IsNullOrWhiteSpace(userCreationRequest.Login) ||
                string.IsNullOrWhiteSpace(userCreationRequest.Password) ||
                string.IsNullOrWhiteSpace(userCreationRequest.PasswordRepetition))
            {
                return HandleBadRequest("A login name and repeat passwords need to be supplied for user creation.");
            }

            if (userCreationRequest.Password != userCreationRequest.PasswordRepetition)
            {
                return HandleBadRequest("Submitted passwords don't match!.");
            }

            if (string.IsNullOrWhiteSpace(userCreationRequest.Name))
            {
                userCreationRequest.Name = userCreationRequest.Login;
            }

            try
            {
                User user = UserService.CreateUser(userCreationRequest.Login, userCreationRequest.Name, userCreationRequest.Password);
                return Created(GetNewResourceUri(user.Login), new UserResponse(user));
            }
            catch (UserAlreadyExistsException exception)
            {
                return HandleResourceAlreadyExistsException(exception);
            }
        }

        [HttpPatch("{login}"), Authorize]
        public IActionResult UpdateUserProfile(string login, [FromBody] UserUpdateRequest userUpdateRequest)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return HandleBadRequest("A valid login name has to be supplied to identify the user to update.");
            }

            if (userUpdateRequest == null)
            {
                return HandleBadRequest("Nothing has been supplied to update.");
            }

            try
            {
                UserService.UpdateUser(login, userUpdateRequest.Name, userUpdateRequest.Biography, userUpdateRequest.Website);
                return Ok();
            }
            catch (UserNotFoundException exception)
            {
                return HandleResourceNotFoundException(exception);
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }

        [HttpPost("roles"), Authorize]
        public IActionResult UpdateUserRoles([FromBody] UpdateUserRoleRequest updateUserRoleRequest)
        {
            if (updateUserRoleRequest == null ||
                string.IsNullOrWhiteSpace(updateUserRoleRequest.Login) ||
                updateUserRoleRequest.RoleIds == null)
            {
                return HandleBadRequest("A valid login name and a (potentially empty) list of user roles need to be supplied.");
            }

            List<Role> roles = new List<Role>();
            try
            {
                foreach (int roleId in updateUserRoleRequest.RoleIds)
                {
                    roles.Add((Role)roleId);
                }
            }
            catch
            {
                return HandleBadRequest("One or more of the supplied IDs is not a valid Sartre role ID.");
            }

            UserService.UpdateUserRoles(updateUserRoleRequest.Login, roles);
            return Ok();
        }
    }
}
