using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskLoggerApi.Data;
using TaskLoggerApi.Models.User;

namespace TaskLoggerApi.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly ApiDbContext _context;
        public BuggyController(ApiDbContext context)
        {
            _context = context;            
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSectret()
        {
            return "Sectret tedt";
        }

        [HttpGet("not-fount")]
        public ActionResult<UserReturnDTO> GetNotFound()
        {
            var thing = _context.Users.Find(-1);

            if (thing == null) return BadRequest();

            return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<UserReturnDTO> GetServerError()
        {
            var thing = _context.Users.Find(-1);

            var thingReturn = thing.ToString();

            return Ok(thingReturn);
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("Bad message request");
        }
    }
}
