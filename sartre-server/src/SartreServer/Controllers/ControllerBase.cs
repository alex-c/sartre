using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SartreServer.Services.Exceptions;
using System;
using System.Linq;
using System.Security.Claims;

namespace SartreServer.Controllers
{
    /// <summary>
    /// Base class for controllers, that helps to handle logging and errors in a unified way.
    /// </summary>
    public class ControllerBase : Controller
    {
        /// <summary>
        /// Logger for controller-level logging.
        /// </summary>
        protected ILogger Logger { get; set; }

        /// <summary>
        /// Get's the URI of a newly created resource, to be included in `201 Created` responses.
        /// </summary>
        /// <param name="resourceId">The ID of the newly created resource.</param>
        /// <returns>Returns the newly created resource's URI.</returns>
        protected Uri GetNewResourceUri(string resourceId)
        {
            return new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{resourceId}");
        }

        /// <summary>
        /// Get's the request subject's name as pasrsed from the JWT, which is the user login name.
        /// </summary>
        /// <returns>Returns the user's login name</returns>
        /// <exception cref="Exception">Thrown if no valid subject name was found.</exception>
        protected string GetSubjectName()
        {
            Claim subject = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            if (subject == null)
            {
                throw new Exception("No subject name available.");
            }
            return subject.Value;
        }

        /// <summary>
        /// Handle bad requests.
        /// </summary>
        /// <param name="message">Message that should explain why the request is bad!</param>
        /// <returns>Returns a 400 error.</returns>
        protected IActionResult HandleBadRequest(string message)
        {
            return BadRequest(message);
        }

        /// <summary>
        /// Handle "resource not found"-type of exceptions
        /// </summary>
        /// <param name="exception">The actual exception.</param>
        /// <returns>Returns a 404 error.</returns>
        protected IActionResult HandleResourceNotFoundException(IResourceNotFoundException exception)
        {
            return NotFound(exception.Message);
        }

        /// <summary>
        /// Handle "resource already exists"-type of exceptions.
        /// </summary>
        /// <param name="exception">The actual exception.</param>
        /// <returns>Returns a 409 error.</returns>
        protected IActionResult HandleResourceAlreadyExistsException(IResourceAlreadyExsistsException exception)
        {
            return Conflict(exception.Message);
        }

        /// <summary>
        /// Handle unexpected exceptions.
        /// </summary>
        /// <param name="exception">Unexpected exception that was caught.</param>
        /// <returns>Returns a 500 error.</returns>
        protected IActionResult HandleUnexpectedException(Exception exception)
        {
            Logger?.LogError(exception, "An unexpected exception was caught.");
            return new StatusCodeResult(500);
        }

        /// <summary>
        /// Handle unexpected exceptions with extra message.
        /// </summary>
        /// <param name="exception">Unexpected exception that was caught.</param>
        /// <param name="message">Extra message explaining the problem.</param>
        /// <returns>Returns a 500 error.</returns>
        protected IActionResult HandleUnexpectedException(Exception exception, string message)
        {
            Logger?.LogError(exception, message);
            return new StatusCodeResult(500);
        }
    }
}
