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

    public class NoteController : BeController
    {
        public NoteController(
            IConfiguration configuration,
            ILogger<NoteController> logger,
            IHttpContextAccessor httpContextAccessor,
            VmEntities context)
            :base(
                configuration: configuration,
                httpContextAccessor: httpContextAccessor,
                logger: logger,
                context: context
            )
        {

        }
        [HttpPost("")]
        public async Task<ActionResult> PostNote([FromBody] Note note)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User user = _auth.GetUser();

            if (isSystem || user != null)
            {
                try
                {
                    _context.Notes.Add(note);
                    _context.SaveChanges();
                    return Ok(note);
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