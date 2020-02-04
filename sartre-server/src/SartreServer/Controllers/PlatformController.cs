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

namespace SartreServer.Controllers
{
    [Route("api/sartre")]
    public class PlatformController : ControllerBase
    {
        private PlatformConfigutationService PlatformConfigutationService { get; }

        private BlogService BlogService { get; }

        public PlatformController(ILoggerFactory loggerFactory, PlatformConfigutationService platformConfigutationService, BlogService blogService)
        {
            Logger = loggerFactory.CreateLogger<PlatformController>();
            PlatformConfigutationService = platformConfigutationService;
            BlogService = blogService;
        }

        #region Public getters

        [HttpGet]
        public IActionResult GetHomePage()
        {
            Blog defaultBlog = PlatformConfigutationService.GetDefaultBlog();

            if (defaultBlog == null)
            {
                IEnumerable<Blog> blogList = BlogService.GetAllBlogs();
                return Ok(new HomePageResponse(blogList));
            }
            else
            {
                return Ok(new HomePageResponse(defaultBlog));
            }
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

        #endregion

        #region Administrative features

        [HttpPost("default"), Authorize(Roles = "Administrator")]
        public IActionResult SetDefaultBlog([FromBody] SetDefaultBlogRequest setDefaultBlogRequest)
        {
            if (setDefaultBlogRequest == null)
            {
                return HandleBadRequest("No data sent on request to set default blog.");
            }

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

        #endregion
    }
}
