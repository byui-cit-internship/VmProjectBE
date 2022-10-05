using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.DAL;
using VmProjectBE.Models;

namespace VmProjectBE.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : BeController
    {

        public UserController(
            IConfiguration configuration,
            ILogger<UserController> logger,
            IHttpContextAccessor httpContextAccessor,
            VmEntities context)
            : base(
                  configuration: configuration,
                  httpContextAccessor: httpContextAccessor,
                  logger: logger,
                  context: context)
        {
        }

        /****************************************
        Returns secions taught by a professor in a given semester
        ****************************************/
        [HttpGet("canvasUsers")]
        public async Task<ActionResult> GetCanvasUsers()
        {
            // Gets email from session
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;
            // Returns a professor user or null if email is not associated with a professor

            if (isSystem)
            {
                // Returns a list of course name, section id, semester, section number, and professor
                // based on the professor and semester variables
                List<User> canvasUsers = (from u in _context.Users
                                          where u.CanvasToken != null
                                          select u).ToList();

                return Ok(canvasUsers);
            }

            else
            {
                return NotFound("Only the BFF application has access to this resource.");
            }
        }
    }
}
