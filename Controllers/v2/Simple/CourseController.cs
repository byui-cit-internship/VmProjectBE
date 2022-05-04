﻿using VmProjectBE.DAL;
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
    public class CourseController : ControllerBase
    {
        private readonly VmEntities _context;
        private readonly ILogger<CourseController> _logger;
        private readonly Authorization _auth;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CourseController(
            VmEntities context,
            ILogger<CourseController> logger,
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
        public async Task<ActionResult> GetCourse(
            [FromQuery] int? courseId,
            [FromQuery] string courseCode,
            [FromQuery] string courseName,
            [FromQuery] int? resourceGroupTemplateId)
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
                    ("courseId", courseId),
                    ("courseCode", courseCode),
                    ("courseName", courseName),
                    ("resourceGroupTemplateId", resourceGroupTemplateId));
                switch (validParameters.Count)
                {
                    case 0:
                        return Ok(
                            (from c in _context.Courses
                             select c).ToList());
                    case 1:
                        switch (validParameters[0])
                        {
                            case "courseId":
                                return Ok(
                                    (from c in _context.Courses
                                     where c.CourseId == courseId
                                     select c).FirstOrDefault());
                            case "courseName":
                                return Ok(
                                    (from c in _context.Courses
                                     where c.CourseName == courseName
                                     select c).FirstOrDefault());

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
        public async Task<ActionResult> PostCourse([FromBody] Course course)
        {
            // Gets email from session
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
                    _context.Courses.Add(course);
                    _context.SaveChanges();
                    return Ok(course);
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