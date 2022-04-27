using DatabaseVmProject.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatabaseVmProject.Services;
using DatabaseVmProject.Models;
using Database_VmProject.Services;
using System.Linq;

namespace DatabaseVmProject.Controllers.v2
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ResourceGroupController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<ResourceGroupController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResourceGroupController(
            VmEntities context,
            ILogger<ResourceGroupController> logger,
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
        public async Task<ActionResult> GetResourceGroup(
            [FromQuery] int? resourceGroupId,
            [FromQuery] string resourceGroupName,
            [FromQuery] double? memory,
            [FromQuery] double? cpu)
        {
            // Gets email from session
            bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == Environment.GetEnvironmentVariable("BFF_PASSWORD");

            int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));

            // Returns a professor user or null if email is not associated with a professor
            User professor = _auth.getAdmin(userId);
            // Returns a professor user or null if email is not associated with a professor

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
                            case "resourceGroupTemplateId":
                                return Ok(
                                    (from rg in _context.ResourceGroups
                                     where rg.ResourceGroupId == resourceGroupId
                                     select rg).FirstOrDefault());
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
            // Gets email from session
            bool isSystem = _httpContextAccessor.HttpContext.Session.GetString("tokenId") == Environment.GetEnvironmentVariable("BFF_PASSWORD");

            int userId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("userId"));

            // Returns a professor user or null if email is not associated with a professor
            User professor = _auth.getAdmin(userId);
            // Returns a professor user or null if email is not associated with a professor

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
