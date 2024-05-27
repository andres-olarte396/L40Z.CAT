using Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace L40Z.CAT.App.API.Controllers
{
    /// <summary>
    /// Controller to handle errors
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Handle error
        /// </summary>
        /// <returns>
        /// Problem
        /// </returns>
        [HttpGet]
        [Route("/error")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            var code = HttpStatusCode.InternalServerError;

            if (exception is NotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is ValidationException) code = HttpStatusCode.BadRequest;

            return Problem(detail: exception?.Message, statusCode: (int)code);
        }
    }
}