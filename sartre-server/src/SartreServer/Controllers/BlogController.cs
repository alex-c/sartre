using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SartreServer.Models;
using SartreServer.Services;
using SartreServer.Services.Exceptions;
using System;
using System.Collections.Generic;

namespace SartreServer.Controllers
{
    [Route("api/blogs")]
    public class BlogController : ControllerBase
    {
        private BlogService BlogService { get; }

        public BlogController(ILoggerFactory loggerFactory, BlogService blogService)
        {
            Logger = loggerFactory.CreateLogger<BlogController>();
            BlogService = blogService;
        }

        [HttpGet]
        public IActionResult GetAllBlogs()
        {
            try
            {
                IEnumerable<Blog> blogs = BlogService.GetAllBlogs();
                return new OkObjectResult(blogs);
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }

        [HttpGet("{blogId}")]
        public IActionResult GetUser(string blogId)
        {
            try
            {
                Blog blog = BlogService.GetBlog(blogId);
                return new OkObjectResult(blog);
            }
            catch (BlogNotFoundException exception)
            {
                return new NotFoundObjectResult(exception.Message);
            }
            catch (Exception exception)
            {
                return HandleUnexpectedException(exception);
            }
        }
    }
}
