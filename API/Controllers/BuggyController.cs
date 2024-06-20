using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("not-found")]
        public ActionResult<string> GetNotFoundRequest()
        {
            return NotFound();
        }
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest(new ProblemDetails{Title = "This is a bad request"});
        }
        [HttpGet("unauthorized")]
        public ActionResult<string> GetUnauthorized()
        {
            return Unauthorized();
        }
        [HttpGet("validation-error")]
        public ActionResult<string> GetValidationError()
        {
            ModelState.AddModelError("problem1", "This is a problem 1");
            ModelState.AddModelError("problem2", "This is a problem 2");
            return ValidationProblem();
        }
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            throw new Exception("This is a server error");
        }



    }
}