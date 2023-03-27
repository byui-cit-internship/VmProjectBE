using Database_VmProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.DAL;
using VmProjectBE.Models;

namespace VmProjectBE.Controllers.v2
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class VmInstanceController : BeController
    {

        public VmInstanceController(
            IConfiguration configuration,
            ILogger<VmInstanceController> logger,
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
        public async Task<ActionResult> GetVmInstance(
            [FromQuery] int? vmInstanceId,
            [FromQuery] string vmTemplateId,
            [FromQuery] string vmInstanceVcenterId,
            [FromQuery] DateTime? vmInstanceExpireDate,
            [FromQuery] string vmInstanceVcenterName,
            [FromQuery] int? userId)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User user = _auth.GetUser();

            if (isSystem || user != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("vmInstanceId", vmInstanceId),
                    ("vmTemplateId", vmTemplateId),
                    ("vmInstanceVcenterId", vmInstanceVcenterId),
                    ("vmInstanceExpireDate", vmInstanceExpireDate),
                    ("vmInstanceVcenterName", vmInstanceVcenterName),
                    ("userId", userId));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from vi in _context.VmInstances
                             select vi).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "vmInstanceId":
                                return Ok(
                                    (from vi in _context.VmInstances
                                     where vi.VmInstanceId == vmInstanceId
                                     select vi).FirstOrDefault());
                            case "vmTemplateId":
                                return Ok(
                                    (from vi in _context.VmInstances
                                     where vi.VmTemplateId == vmTemplateId
                                     select vi).ToList());
                            case "vmInstanceVcenterId":
                                return Ok(
                                    (from vi in _context.VmInstances
                                     where vi.VmInstanceVcenterId == vmInstanceVcenterId
                                     select vi).FirstOrDefault());
                            case "vmInstanceVcenterName":
                                return Ok(
                                    (from vi in _context.VmInstances
                                     where vi.VmInstanceVcenterName == vmInstanceVcenterName
                                     select vi).FirstOrDefault());
                            case "userId":
                                return Ok(
                                    (from  vi in _context.VmInstances where vi.UserId == userId select vi).ToList());
                                    // join tu in _context.TagUsers
                                    // on u.UserId equals tu.UserId
                                    // join t in _context.Tags
                                    // on tu.TagId equals t.TagId
                                    // join tc in _context.TagCategories
                                    // on t.TagCategoryId equals tc.TagCategoryId
                                    // join vit in _context.VmInstanceTags
                                    // on t.TagId equals vit.TagId
                                    // join vi in _context.VmInstances
                                    // on vit.VmInstanceId equals vi.VmInstanceId
                                    // where u.UserId == userId
                                    // where tc.TagCategoryName == "User"
                                    // select vi).ToList());
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
        public async Task<ActionResult> PostVmInstance([FromBody] VmInstance vmInstance)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User user = _auth.GetUser();

            if (isSystem || user != null)
            {
                try
                {
                    _context.VmInstances.Add(vmInstance);
                    _context.SaveChanges();
                    return Ok(vmInstance);
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
