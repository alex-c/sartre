﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SartreServer.Models;
using SartreServer.Services;
using SartreServer.Services.Exceptions;
using System;
using System.Collections.Generic;

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
                return Ok(users);
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
                return Ok(user);
            }
            catch (UserNotFoundException exception)
            {
                return HandleNotFoundException(exception);
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }
    }
}
