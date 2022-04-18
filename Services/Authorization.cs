using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using DatabaseVmProject.DAL;
using DatabaseVmProject.Models;

namespace DatabaseVmProject.Services
{
    public class Authorization
    {
        private readonly VmEntities _context;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        public Authorization(VmEntities context, ILogger logger, IWebHostEnvironment env) 
            : this(context, logger)
        {
            _env = env;
        }

        public Authorization(VmEntities context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }



        /****************************************
        Given an email returns either a professor user or null if the email doesn't belong to a professor
        ****************************************/
        public User getProfessor(int userId, int sectionId)
        {
            User professor = (from u in _context.Users
                              join usr in _context.UserSectionRoles
                              on u.UserId equals usr.UserId
                              join r in _context.Roles
                              on usr.RoleId equals r.RoleId
                              join s in _context.Sections
                              on usr.SectionId equals s.SectionId
                              where r.RoleName == "Professor"
                              && u.UserId == userId
                              && s.SectionId == sectionId
                              select u).FirstOrDefault();
            return professor;
        }

        /****************************************
        Given an email returns either a user or null if no user exists with the given email
        ****************************************/
        public User getUser(int userId)
        {
            User user = (from u in _context.Users
                         where u.UserId == userId
                         select u).FirstOrDefault();
            return user;
        }

        /****************************************
        Given an email returns either an admin user or null if no admin user exists with the given email
        ****************************************/
        public User getAdmin(int userId)
        {
            User admin = (from u in _context.Users
                          where u.IsAdmin
                          where u.UserId == userId
                          select u).FirstOrDefault();
            return admin;
        }
    }
}
