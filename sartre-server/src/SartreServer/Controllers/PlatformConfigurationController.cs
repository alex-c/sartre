using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SartreServer.Contracts;
using SartreServer.Models;
using SartreServer.Services;
using SartreServer.Services.Exceptions;
using System;

namespace SartreServer.Controllers
{
    [Route("api/sartre")]
    public class PlatformConfigurationController : ControllerBase
    {
        private PlatformConfigutationService PlatformConfigutationService { get; }

        public PlatformConfigurationController(ILoggerFactory loggerFactory, PlatformConfigutationService platformConfigutationService)
        {
            Logger = loggerFactory.CreateLogger<PlatformConfigurationController>();
            PlatformConfigutationService = platformConfigutationService;
        }

        [HttpGet("default")]
        public IActionResult GetDefaultBlog()
        {
            try
            {
                Blog defaultBlog = PlatformConfigutationService.GetDefaultBlog();
                return Ok(defaultBlog);
            }
            catch (BlogNotFoundException exception)
            {
                return HandleUnexpectedException(exception, "Inconsistent state: there is a default blog ID set, but the blog could not be found!");
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }

        [HttpPost("default")]
        public IActionResult SetDefaultBlog([FromBody] SetDefaultBlogRequest setDefaultBlogRequest)
        {
            try
            {
                PlatformConfigutationService.SetDefaultBlog(setDefaultBlogRequest.BlogId);
                return Ok();
            }
            catch (BlogNotFoundException exception)
            {
                return HandleBadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }
    }
}
