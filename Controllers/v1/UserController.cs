using DatabaseVmProject.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatabaseVmProject.Services;
using DatabaseVmProject.Models;

namespace DatabaseVmProject.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<UserController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(VmEntities context, ILogger<UserController> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {

            _context = context;
            _logger = logger;
            _auth = new(_context, _logger);
            _httpContextAccessor = httpContextAccessor;
            _env = env;
        }

        /****************************************
        Returns secions taught by a professor in a given semester
        ****************************************/
        [HttpGet("canvasUsers")]
        public async Task<ActionResult> GetCanvasUsers()
        {
            // Gets email from session
            bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == Environment.GetEnvironmentVariable("BFF_PASSWORD");

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
