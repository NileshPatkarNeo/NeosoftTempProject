using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebApiCleanWithMediatr.Identity;
using WebApiCleanWithMediatr.Identity.Models;

namespace WebApiCleanWithMediatr.Utitlity
{
    public class DBSeeding : IDBSeeding
    {

        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IdentityDbContextNew _context;

        public DBSeeding(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IdentityDbContextNew context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }

            //Check if default Admin user exists or not
            if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                //Create User Roles in the database
                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("Student")).GetAwaiter().GetResult();

                //Note: Password should always have one Capital alphabet and one Special Character
                _userManager.CreateAsync(new AppUser
                {
                    UserName = "dhanshreetekale",
                    Email = "dhanshreetekale@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    //Password = "Admin@123"
                }, "Admin@123").GetAwaiter().GetResult();

                //Get the default Admin User, which is created above.
                var AppuserNew = _context.Users.FirstOrDefault(x => x.Email == "dhanshreetekale@gmail.com");
                if (AppuserNew != null)
                {
                    _userManager.AddToRoleAsync((AppUser)AppuserNew, "Admin").GetAwaiter().GetResult();
                }
            }
        }

    }


    public interface IDBSeeding
    {
        void Initialize();
    }
}
