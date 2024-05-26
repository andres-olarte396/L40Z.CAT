using Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace L40Z.CAT.App.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ErrorController : ControllerBase
    {
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