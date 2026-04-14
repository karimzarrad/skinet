using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class BuggyController :BaseApiController
    {
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }
        [HttpGet("internalerror")]
        public IActionResult GetInternalError()
        {
           throw new Exception("this is a test exception");
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
           return BadRequest("Not a good request");
        }
        [HttpPost("validationerror")]
        public IActionResult GetValidationError(CreateProductDto productDto)
        {
            return Ok();
        }
    }
}
