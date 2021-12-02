using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement
{
    public static class SeedData
    {
        public static void Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> rolemanager)
        {
            SeedRoles(rolemanager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if(userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new IdentityUser();
                user.UserName = "admin";
                user.Email = "admin@mail.com";
                var result = userManager.CreateAsync(user, "P@ssword1").Result;
                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator");
                }

                var users = userManager.Users;
                var usersList = users.AsEnumerable().ToList();
                foreach(var existUser in usersList)
                {
                    userManager.AddToRoleAsync(existUser, "Employee");
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole();
                role.Name = "Administrator";
                var result =  roleManager.CreateAsync(role).Result;

            }

            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                var result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
