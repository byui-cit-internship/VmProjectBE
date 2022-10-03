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
    public class UserSectionRoleController : BeController
    {

        public UserSectionRoleController(
            IConfiguration configuration,
            ILogger<UserSectionRoleController> logger,
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
        public async Task<ActionResult> GetUserSectionRole(
            [FromQuery] int? userSectionRoleId,
            [FromQuery] int? userId,
            [FromQuery] int? sectionId,
            [FromQuery] int? roleId)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

            if (isSystem || professor != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("userSectionRoleId", userSectionRoleId),
                    ("userId", userId),
                    ("sectionId", sectionId),
                    ("roleId", roleId));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from usr in _context.UserSectionRoles
                             select usr).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "userSectionRoleId":
                                return Ok(
                                    (from usr in _context.UserSectionRoles
                                     where usr.UserSectionRoleId == userSectionRoleId
                                     select usr).FirstOrDefault());
                            case "userId":
                                return Ok(
                                    (from usr in _context.UserSectionRoles
                                     where usr.UserId == userId
                                     select usr).ToList());
                            case "sectionId":
                                return Ok(
                                    (from usr in _context.UserSectionRoles
                                     where usr.SectionId == sectionId
                                     select usr).ToList());
                            case "roleId":
                                return Ok(
                                    (from usr in _context.UserSectionRoles
                                     where usr.RoleId == roleId
                                     select usr).ToList());
                            default:
                                return BadRequest("How did you get here?");
                        }
                    case 2:
                        switch (true)
                        {
                            case bool ifTrue when 
                            validParameters.Contains("userId") &&
                            validParameters.Contains("sectionId"):
                                return Ok(
                                    (from usr in _context.UserSectionRoles
                                     where usr.UserId == userId
                                     && usr.SectionId == sectionId
                                     select usr).ToList());
                            case bool ifTrue when
                            validParameters.Contains("userId") &&
                            validParameters.Contains("roleId"):
                                return Ok(
                                    (from usr in _context.UserSectionRoles
                                     where usr.UserId == userId
                                     && usr.RoleId == roleId
                                     select usr).ToList());
                            case bool ifTrue when
                            validParameters.Contains("sectionId") &&
                            validParameters.Contains("roleId"):
                                return Ok(
                                    (from usr in _context.UserSectionRoles
                                     where usr.SectionId == sectionId
                                     && usr.RoleId == roleId
                                     select usr).ToList());
                            default:
                                return BadRequest("How did you get here?");
                        }
                    case 3:
                        switch (true)
                        {
                            case bool ifTrue when
                            validParameters.Contains("userId") &&
                            validParameters.Contains("sectionId") &&
                            validParameters.Contains("roleId"):
                                return Ok(
                                    (from usr in _context.UserSectionRoles
                                     where usr.UserId == userId
                                     && usr.SectionId == sectionId
                                     && usr.RoleId == roleId
                                     select usr).FirstOrDefault());
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
        public async Task<ActionResult> PostUserSectionRole([FromBody] UserSectionRole userSectionRole)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

            if (isSystem || professor != null)
            {
                try
                {
                    _context.UserSectionRoles.Add(userSectionRole);
                    _context.SaveChanges();
                    return Ok(userSectionRole);
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
