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
        public async Task<ActionResult> GetVmTemplate(
            [FromQuery] int? vmInstanceId,
            [FromQuery] int? vmTemplateId,
            [FromQuery] string vmInstanceVcenterId,
            [FromQuery] DateTime? vmInstanceExpireDate)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

            if (isSystem || professor != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("vmInstanceId", vmInstanceId),
                    ("vmTemplateId", vmTemplateId),
                    ("vmInstanceVcenterId", vmInstanceVcenterId),
                    ("vmInstanceExpireDate", vmInstanceExpireDate));
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
