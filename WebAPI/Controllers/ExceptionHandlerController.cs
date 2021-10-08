using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ExceptionHandlerController : ControllerBase
    {
        private ILogger<ExceptionHandlerController> logger;
        public ExceptionHandlerController(ILogger<ExceptionHandlerController> logger)
        {
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult> OnGet()
        {
            Exception exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            logger.LogError($"{exception.GetType()} {exception.Message}");

            return Problem(statusCode: StatusCodes.Status500InternalServerError, title: exception.Message, type: exception.GetType().Name);
        }
    }
}
