using Database_VmProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.DAL;
using VmProjectBE.Models;
using VmProjectBE.DTO;

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
        [HttpGet("")]
        public async Task<ActionResult> GetNote(
            [FromQuery] int notesId,
            [FromQuery] string noteDetail,
            [FromQuery] int  section)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User user = _auth.GetUser();

            if (isSystem || user != null)
            {
                List<string> validParameters = QueryParamHelper.ValidateParameters(
                    ("notesId", notesId),
                    ("noteDetail", noteDetail),
                    ("setion", section));
                switch (validParameters.Count)
                {
                    case 0 : 
                        return Ok(
                            (from n in _context.Notes
                            select n).ToList());
                    case 1 : 
                        switch (validParameters[0])
                        {
                            case "noteId":
                                return Ok(
                                    (from n in _context.Notes
                                     where n.NotesId == notesId
                                     select n).FirstOrDefault());
                            case "noteDetail":
                                return Ok(
                                    (from n in _context.Notes
                                     where n.NoteDetail == noteDetail
                                     select n).FirstOrDefault());
                                

                                
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
        [HttpPost("")]
        public async Task<ActionResult> PostNote([FromBody] NoteDTO noteDTO)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User user = _auth.GetUser();

            if (isSystem || user != null)
            {
                try
                {
                    Note noteModel = new Note();
                    noteModel.NoteDetail = noteDTO.NoteDetail;
                    _context.Notes.Add(noteModel);
                    var section = _context.Sections.FirstOrDefault(section=>section.SectionId==noteDTO.SectionId);
                    noteModel.Section=section;
                    _context.SaveChanges();
                    return Ok(noteModel);
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