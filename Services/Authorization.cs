using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using VmProjectBE.DAL;
using VmProjectBE.Models;

namespace VmProjectBE.Services
{
    public class Authorization
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly string _vimaCookie;
        private readonly VmEntities _context;


        public Authorization(
            IConfiguration configuration,
            ILogger logger,
            string vimaCookie,
            VmEntities context)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
            _vimaCookie = vimaCookie;
        }



        /****************************************
        Given an email returns either a professor user or null if the email doesn't belong to a professor
        ****************************************/
        public User GetProfessor(int sectionId)
        {
            if (_vimaCookie == _configuration.GetConnectionString("BFF_PASSWORD"))
            {
                return null;
            }
            User professor = (from st in _context.SessionTokens
                              join at in _context.AccessTokens
                              on st.AccessTokenId equals at.AccessTokenId
                              join u in _context.Users
                              on at.UserId equals u.UserId
                              join usr in _context.UserSectionRoles
                              on u.UserId equals usr.UserId
                              join r in _context.Roles
                              on usr.RoleId equals r.RoleId
                              join s in _context.Sections
                              on usr.SectionId equals s.SectionId
                              where r.RoleName == "Professor"
                              && s.SectionId == sectionId
                              && st.SessionTokenValue == Guid.Parse(_vimaCookie)
                              select u).FirstOrDefault();
            return professor;
        }

        /****************************************
        Given an email returns either a user or null if no user exists with the given email
        ****************************************/
        public User GetUser()
        {
            if (_vimaCookie == _configuration.GetConnectionString("BFF_PASSWORD"))
            {
                return null;
            }
            User user = (from st in _context.SessionTokens
                         join at in _context.AccessTokens
                         on st.AccessTokenId equals at.AccessTokenId
                         join u in _context.Users
                         on at.UserId equals u.UserId
                         where st.SessionTokenValue == Guid.Parse(_vimaCookie)
                         select u).FirstOrDefault();
            return user;
        }

        /****************************************
        Given an email returns either an admin user or null if no admin user exists with the given email
        ****************************************/
        public User GetAdmin()
        {
            if (_vimaCookie == _configuration.GetConnectionString("BFF_PASSWORD"))
            {
                return null;
            }
            User admin = (from st in _context.SessionTokens
                          join at in _context.AccessTokens
                          on st.AccessTokenId equals at.AccessTokenId
                          join u in _context.Users
                          on at.UserId equals u.UserId
                          where st.SessionTokenValue == Guid.Parse(_vimaCookie)
                          where u.IsAdmin
                          select u).FirstOrDefault();
            return admin;
        }
    }
}
