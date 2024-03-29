﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VmProjectBE.DAL;
using VmProjectBE.DTO.v1;
using VmProjectBE.Models;

namespace VmProjectBE.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CreateVmController : BeController
    {

        public CreateVmController(
            IConfiguration configuration,
            ILogger<CreateVmController> logger,
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
        Returns secions taught by a professor in a given semester
        ****************************************/
        [HttpGet("")]
        public async Task<ActionResult> GetCanvasUsers([FromQuery] int enrollmentId)
        {
            // Gets email from session
            // Returns a professor user or null if email is not associated with a professor

            // Returns a list of course name, section id, semester, section number, and professor
            // based on the professor and semester variables
            List<CreateVmDTO> createVm = (from usr in _context.UserSectionRoles
                                          where usr.UserSectionRoleId == enrollmentId
                                          join u in _context.Users 
                                          on usr.UserId equals u.UserId
                                          join sec in _context.Sections
                                          on usr.SectionId equals sec.SectionId
                                          join f in _context.Folders
                                          on sec.FolderId equals f.FolderId
                                          join c in _context.Courses
                                          on sec.CourseId equals c.CourseId
                                          join sem in _context.Semesters
                                          on sec.SemesterId equals sem.SemesterId
    
                                          where usr.UserSectionRoleId == enrollmentId

                                          select new CreateVmDTO(
                                              $"{u.FirstName} {u.LastName}",
                                              sec.SectionName,
                                              c.CourseId,
                                              sem.SemesterTerm,
                                              usr.UserSectionRoleId,
                                              f.VcenterFolderId,
                                              sec.LibraryVCenterId
                                          )).ToList();

            return Ok(createVm);
        }

        [HttpPost("")]
        public async Task<ActionResult> PostCreateVm([FromBody] VmInstance vmInstance)
        {
            string bffPassword = _configuration.GetConnectionString("BFF_PASSWORD");
            bool isSystem = bffPassword == _vimaCookie;

            User user = _auth.GetUser();
            try
            {   //removed the tags from here
                // TagCategory userTC = (from tc in _context.TagCategories
                //                       where tc.TagCategoryName == "User"
                //                       select tc).FirstOrDefault();
                // if (userTC == null)
                // {
                //     userTC = new();
                //     userTC.TagCategoryName = "User";
                //     userTC.TagCategoryVcenterId = "DONT_USE";
                //     userTC.TagCategoryDescription = "For temporary use.";

                //     _context.TagCategories.Add(userTC);
                //     _context.SaveChanges();
                // }

                // Tag userTag = (from t in _context.Tags
                //                where t.TagName == user.Email
                //                select t).FirstOrDefault();

                // if (userTag == null)
                // {
                //     userTag = new();
                //     userTag.TagName = user.Email;
                //     userTag.TagCategoryId = userTC.TagCategoryId;
                //     userTag.TagVcenterId = "DONT_USE";

                //     _context.Tags.Add(userTag);
                //     _context.SaveChanges();
                // }

                // TagUser tagUser = (from tu in _context.TagUsers
                //                    where tu.UserId == user.UserId
                //                    && tu.TagId == userTag.TagId
                //                    select tu).FirstOrDefault();

                // if (tagUser == null)
                // {
                //     tagUser = new();
                //     tagUser.TagId = userTag.TagId;
                //     tagUser.UserId = user.UserId;

                //     _context.TagUsers.Add(tagUser);
                //     _context.SaveChanges();
                // }
                // "{\"error_type\":\"INVALID_ARGUMENT\",\"messages\":[{\"args\":[\"CIT270:261d8ca6-5f51-408b-9f00-6179577b2333\"],\"default_message\":\"The provided folder ID CIT270:261d8ca6-5f51-408b-9f00-6179577b2333 is invalid.\",\"id\":\"com.vmware.vdcs.vmtx-main.invalid_folder_id_format\"}]}"
                _context.VmInstances.Add(vmInstance);
                _context.SaveChanges();

                // VmInstanceTag vmInstanceTag = new();
                // vmInstanceTag.VmInstanceId = vmInstance.VmInstanceId;
                // vmInstanceTag.TagId = userTag.TagId;

                // _context.VmInstanceTags.Add(vmInstanceTag);
                // _context.SaveChanges();

                return Ok(vmInstance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
