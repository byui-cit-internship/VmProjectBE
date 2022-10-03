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
    public class RoleController : BeController
    {

        public RoleController(
            IConfiguration configuration,
            ILogger<RoleController> logger,
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

        ****************************************/
        [HttpGet("")]
        public async Task<ActionResult> GetRole(
            [FromQuery] int? roleId,
            [FromQuery] string roleName,
            [FromQuery] int? canvasRoleId)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

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
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

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
