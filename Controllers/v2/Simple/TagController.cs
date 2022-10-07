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
    public class TagController : BeController
    {

        public TagController(
            IConfiguration configuration,
            ILogger<TagController> logger,
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
        public async Task<ActionResult> GetTag(
            [FromQuery] int? tagId,
            [FromQuery] int? tagCategoryId,
            [FromQuery] string tagVcenterId,
            [FromQuery] string tagName,
            [FromQuery] string tagDescription)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

            if (isSystem || professor != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("tagId", tagId),
                    ("tagCategoryId", tagCategoryId),
                    ("tagVcenterId", tagVcenterId),
                    ("tagName", tagName),
                    ("tagDescription", tagDescription));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from t in _context.Tags
                             select t).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "tagId":
                                return Ok(
                                    (from t in _context.Tags
                                     where t.TagId == tagId
                                     select t).FirstOrDefault());
                            case "tagCategoryId":
                                return Ok(
                                    (from t in _context.Tags
                                     where t.TagCategoryId == tagCategoryId
                                     select t).ToList());
                            case "tagVcenterId":
                                return Ok(
                                    (from t in _context.Tags
                                     where t.TagVcenterId == tagVcenterId
                                     select t).FirstOrDefault());
                            case "tagName":
                                return Ok(
                                    (from t in _context.Tags
                                     where t.TagName == tagName
                                     select t).ToList());
                            default:
                                return BadRequest("Invalid single parameter. Check documentation.");
                        }
                    case 2:
                        switch (true)
                        {
                            case bool ifTrue when
                                validParameters.Contains("tagCategoryId") &&
                                validParameters.Contains("tagName"):
                                return Ok(
                                    (from t in _context.Tags
                                     where t.TagCategoryId == tagCategoryId
                                     && t.TagName == tagName
                                     select t).FirstOrDefault());
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
        public async Task<ActionResult> PostTag([FromBody] Tag tag)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User professor = _auth.GetAdmin();

            if (isSystem || professor != null)
            {
                try
                {
                    _context.Tags.Add(tag);
                    _context.SaveChanges();
                    return Ok(tag);
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
