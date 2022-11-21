﻿using Database_VmProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.DAL;
using VmProjectBE.Models;

namespace VmProjectBE.Controllers.v2
{
    [Authorize]
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ResourcePoolController : BeController
    {

        public ResourcePoolController(
            IConfiguration configuration,
            ILogger<ResourcePoolController> logger,
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
        public async Task<ActionResult> GetResourcePool(
            [FromQuery] int? resourcePoolId,
            [FromQuery] string resourcePoolName,
            [FromQuery] double? memory,
            [FromQuery] double? cpu)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

            if (isSystem || professor != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("resourcePoolId", resourcePoolId),
                    ("resourcePoolName", resourcePoolName),
                    ("memory", memory),
                    ("cpu", cpu));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from rg in _context.ResourcePools
                             select rg).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "resourcePoolId":
                                return Ok(
                                    (from rg in _context.ResourcePools
                                     where rg.ResourcePoolId == resourcePoolId
                                     select rg).FirstOrDefault());
                            case "resourcePoolName":
                                return Ok(
                                    (from rg in _context.ResourcePools
                                     where rg.ResourcePoolName == resourcePoolName
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
                            validParameters.Contains("resourcePoolName"):
                                return Ok(
                                    (from rg in _context.ResourcePools
                                     where rg.Memory.Equals(memory)
                                     && rg.Cpu.Equals(cpu)
                                     && rg.ResourcePoolName == resourcePoolName
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
        public async Task<ActionResult> PostResourcePool([FromBody] ResourcePool resourcePool)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

            if (isSystem || professor != null)
            {
                try
                {
                    _context.ResourcePools.Add(resourcePool);
                    _context.SaveChanges();
                    return Ok(resourcePool);
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