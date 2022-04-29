using VmProjectBE.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.Services;

namespace VmProjectBE.Controllers.v1
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<AuthorizationController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationController(
            VmEntities context, 
            ILogger<AuthorizationController> logger,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _auth = new(_context, _logger);
            _httpContextAccessor = httpContextAccessor;
            _env = env;
        }

        /****************************************
        
        ****************************************/
        [HttpGet()]
        public async Task<ActionResult> AuthorizeUsers(
            [FromQuery] string authType,
            [FromQuery] int? sectionId = null)
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
            if (authType != null)
            {
                authType = authType.ToLower();
                if (authType == "admin")
                {
                    return Ok(_auth.getAdmin(userId));
                }
                else if (authType == "professor")
                {
                    if (sectionId != null)
                    {
                        return Ok(_auth.getProfessor(userId, (int)sectionId));
                    }
                    else
                    {
                        return BadRequest("When authorizing a professor a sectionId must be present.");
                    }
                }
                else if (authType == "user")
                {
                    return Ok(_auth.getUser(userId));
                }
                else
                {
                    return BadRequest("AuthType must be either user, professor, or admin.");
                }
            }
            else
            {
                return BadRequest("AuthType is required and must be either user, professor, or admin.");
            }

        }
    }
}
