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
    public class TagUserController : BeController
    {

        public TagUserController(
            IConfiguration configuration,
            ILogger<TagUserController> logger,
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
        public async Task<ActionResult> GetTagUser(
            [FromQuery] int? tagUserId,
            [FromQuery] int? tagId,
            [FromQuery] int? userId)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

            if (isSystem || professor != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("tagUserId", tagUserId),
                    ("tagId", tagId),
                    ("userId", userId));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from tu in _context.TagUsers
                             select tu).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "tagUserId":
                                return Ok(
                                    (from tu in _context.TagUsers
                                     where tu.TagUserId == tagUserId
                                     select tu).FirstOrDefault());
                            case "tagId":
                                return Ok(
                                    (from tu in _context.TagUsers
                                     where tu.TagId == tagId
                                     select tu).ToList());
                            case "userId":
                                return Ok(
                                    (from tu in _context.TagUsers
                                     where tu.UserId == userId
                                     select tu).ToList());
                            default:
                                return BadRequest("Invalid single parameter. Check documentation.");
                        }
                    case 2:
                        switch (true)
                        {
                            case bool ifTrue when
                            validParameters.Contains("tagId") &&
                            validParameters.Contains("userId"):
                                return Ok(
                                    (from tu in _context.TagUsers
                                     where tu.TagId == tagId
                                     && tu.UserId == userId
                                     select tu).FirstOrDefault());
                            default:
                                return BadRequest("Invalid single parameter. Check documentation.");
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
        public async Task<ActionResult> PostTagUser([FromBody] TagUser tagUser)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

            if (isSystem || professor != null)
            {
                try
                {
                    _context.TagUsers.Add(tagUser);
                    _context.SaveChanges();
                    return Ok(tagUser);
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
