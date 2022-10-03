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
    public class ResourceGroupController : BeController
    {

        public ResourceGroupController(
            IConfiguration configuration,
            ILogger<ResourceGroupController> logger,
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
        public async Task<ActionResult> GetResourceGroup(
            [FromQuery] int? resourceGroupId,
            [FromQuery] string resourceGroupName,
            [FromQuery] double? memory,
            [FromQuery] double? cpu)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

            if (isSystem || professor != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("resourceGroupId", resourceGroupId),
                    ("resourceGroupName", resourceGroupName),
                    ("memory", memory),
                    ("cpu", cpu));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from rg in _context.ResourceGroups
                             select rg).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "resourceGroupId":
                                return Ok(
                                    (from rg in _context.ResourceGroups
                                     where rg.ResourceGroupId == resourceGroupId
                                     select rg).FirstOrDefault());
                            case "resourceGroupName":
                                return Ok(
                                    (from rg in _context.ResourceGroups
                                     where rg.ResourceGroupName == resourceGroupName
                                     select rg).ToList());
                            default:
                                return BadRequest("Incorrect parameters entered");
                        }
                    case 3:
                        switch (true)
                        {
                            case bool ifTrue when
                            validParameters.Contains("memory") &&
                            validParameters.Contains("cpu") &&
                            validParameters.Contains("resourceGroupName"):
                                return Ok(
                                    (from rg in _context.ResourceGroups
                                     where rg.Memory.Equals(memory)
                                     && rg.Cpu.Equals(cpu)
                                     && rg.ResourceGroupName == resourceGroupName
                                     select rg).FirstOrDefault());
                            default:
                                return BadRequest("Incorrect parameters entered");
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
        public async Task<ActionResult> PostResourceGroup([FromBody] ResourceGroup resourceGroup)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

            if (isSystem || professor != null)
            {
                try
                {
                    _context.ResourceGroups.Add(resourceGroup);
                    _context.SaveChanges();
                    return Ok(resourceGroup);
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
