using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SartreServer.Models;
using SartreServer.Services;
using SartreServer.Services.Exceptions;
using System;
using System.Collections.Generic;

namespace SartreServer.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private ILogger Logger { get; }

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
                return new OkObjectResult(users);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("{login}")]
        public IActionResult GetUser(string login)
        {
            try
            {
                User user = UserService.GetUser(login);
                return new OkObjectResult(user);
            }
            catch (UserNotFoundException exception)
            {
                return new NotFoundObjectResult(exception.Message);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
