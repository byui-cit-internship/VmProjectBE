using Database_VmProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.DAL;
using VmProjectBE.DTO;

namespace VmProjectBE.Controllers.v2
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class UserVmInstanceController : BeController
    {

        public UserVmInstanceController(
            IConfiguration configuration,
            ILogger<UserVmInstanceController> logger,
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
        [AllowAnonymous]
        public async Task<ActionResult> GetUserVmInstance(
            [FromQuery] int? userId,
            [FromQuery] int? vmInstanceId,
            [FromQuery] int? sectionId)
        {
            // Gets email from session
            //bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == Environment.GetEnvironmentVariable("BFF_PASSWORD");

            //int accessUserId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));

            // Returns a professor user or null if email is not associated with a professor
            // User professor = _auth.getAdmin(accessUserId);
            // Returns a professor user or null if email is not associated with a professor

            //if (isSystem || professor != null)
            //{
            List<string> validParameters = QueryParamHelper.ValidateParameters(
                ("userId", userId),
                ("vmInstanceId", vmInstanceId),
                ("sectionId", sectionId));
            switch (validParameters.Count)
            {
                case 0:
                    return BadRequest("Must provide at least one parameter");
                case 1:
                    switch (validParameters[0])
                    {
                        case "userId":
                            return Ok(
                                (from u in _context.Users
                                join tu in _context.TagUsers
                                on u.UserId equals tu.UserId
                                join t in _context.Tags
                                on tu.TagId equals t.TagId
                                join tc in _context.TagCategories
                                on t.TagCategoryId equals tc.TagCategoryId
                                join vit in _context.VmInstanceTags
                                on t.TagId equals vit.TagId
                                join vi in _context.VmInstances
                                on vit.VmInstanceId equals vi.VmInstanceId
                                where u.UserId == userId
                                where tc.TagCategoryName == "User"
                                select new UserVmInstance(
                                    u,
                                    vi
                                )).ToList());
                        case "vmInstanceId":
                            return Ok();
                        case "sectionId":
                            return Ok();
                        default:
                            return BadRequest("Invalid single parameter. Check documentation.");
                    }
                default:
                    return BadRequest("Incorrect parameters entered");
            }
        }
    }
}
