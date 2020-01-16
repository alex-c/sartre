using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace SartreServer.Controllers
{

    public class ControllerBase : Controller
    {
        protected ILogger Logger { get; set; }

        protected IActionResult HandleUnexpectedException(Exception exception)
        {
            Logger?.LogError(exception, "An unexpected exception was caught.");
            return new StatusCodeResult(500);
        }

        protected IActionResult HandleUnexpectedException(Exception exception, string message)
        {
            Logger?.LogError(exception, message);
            return new StatusCodeResult(500);
        }
    }
}
