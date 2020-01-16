using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SartreServer.Services.Exceptions;
using System;

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
        /// Handle 400 errors!
        /// </summary>
        /// <param name="message">Message that should explain why the request is bad!</param>
        /// <returns>Returns a 400 error.</returns>
        protected IActionResult HandleBadRequest(string message)
        {
            return BadRequest(message);
        }

        /// <summary>
        /// Handle 404 errors!
        /// </summary>
        /// <param name="exception">Not-found exception.</param>
        /// <returns>Returns a 404 error.</returns>
        protected IActionResult HandleNotFoundException(INotFoundException exception)
        {
            return NotFound(exception.Message);
        }


        /// <summary>
        /// Handle 500 errors for totally unexpected exceptions!
        /// </summary>
        /// <param name="exception">Unexpected exception that was caught.</param>
        /// <returns>Returns a 500 error.</returns>
        protected IActionResult HandleUnexpectedException(Exception exception)
        {
            Logger?.LogError(exception, "An unexpected exception was caught.");
            return new StatusCodeResult(500);
        }

        /// <summary>
        /// Handle 500 errors for an expected exception with extra message!
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
