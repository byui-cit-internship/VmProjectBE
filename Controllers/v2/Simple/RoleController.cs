using VmProjectBE.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.Services;
using VmProjectBE.Models;
using Database_VmProject.Services;
using System.Linq;

namespace VmProjectBE.Controllers.v2
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<RoleController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleController(
            VmEntities context,
            ILogger<RoleController> logger,
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
        [HttpGet("")]
        public async Task<ActionResult> GetRole(
            [FromQuery] int? roleId,
            [FromQuery] string roleName,
            [FromQuery] int? canvasRoleId)
        {
            // Gets email from session
            bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == Environment.GetEnvironmentVariable("BFF_PASSWORD");
            User professor = null;

            if (!isSystem)
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
                professor = _auth.getAdmin(userId);
            }

            // Returns a professor user or null if email is not associated with a professor
            // Returns a professor user or null if email is not associated with a professor

            if (isSystem || professor != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("roleId", roleId),
                    ("roleName", roleName),
                    ("canvasRoleId", canvasRoleId));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from r in _context.Roles
                             select r).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "roleId":
                                return Ok(
                                    (from r in _context.Roles
                                     where r.RoleId == roleId
                                     select r).FirstOrDefault());
                            case "roleName":
                                return Ok(
                                    (from r in _context.Roles
                                     where r.RoleName == roleName
                                     select r).ToList());
                            case "canvasRoleId":
                                return Ok(
                                    (from r in _context.Roles
                                     where r.CanvasRoleId == canvasRoleId
                                     select r).ToList());
                            default:
                                return BadRequest("How did you get here?");
                        }
                    default:
                        return BadRequest("Incorrect parameters entered");
                }
            }
            else
            {
                return NotFound("Only the BFF application has access to this resource.");
            }
        }

        /****************************************

        ****************************************/
        [HttpPost("")]
        public async Task<ActionResult> PostRole([FromBody] Role role)
        {
            // Gets email from session
            bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == Environment.GetEnvironmentVariable("BFF_PASSWORD");
            User professor = null;

            if (!isSystem)
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));
                professor = _auth.getAdmin(userId);
            }

            if (isSystem || professor != null)
            {
                try
                {
                    _context.Roles.Add(role);
                    _context.SaveChanges();
                    return Ok(role);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Only the BFF application has access to this resource.");
            }
        }
    }
}
