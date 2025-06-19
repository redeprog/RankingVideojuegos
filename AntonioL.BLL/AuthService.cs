using AntonioL.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonioL.BLL
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserManager<ApplicationUser> UserManager => _userManager;

        public AuthService()
        {
            var ctx = new ApplicationDbContext();
            var store = new UserStore<ApplicationUser>(ctx);
            _userManager = new UserManager<ApplicationUser>(store);
        }

        public IdentityResult Register(string email, string password, string role)
        {
            var user = new ApplicationUser { UserName = email, Email = email };
            var result = _userManager.Create(user, password);
            if (result.Succeeded)
                _userManager.AddToRole(user.Id, role);
            return result;
        }

        public ApplicationUser Login(string email, string password)
        {
            return _userManager.Find(email, password);
        }
    }
}
