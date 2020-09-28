using Microsoft.AspNetCore.Identity;
using NewsFeed.Models.Identity;
using System.Linq;

namespace NewsFeed.Models.Data
{
    public static class IdentityDbInitializer
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.ApplicationUsers.Any())
            {
                return;   // DB has been seeded
            }

            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Reader").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Reader";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Publisher").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Publisher";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "user1",
                Email = "user1@localhost",
                FullName = "Peter Greenaway"
            };

            if (userManager.CreateAsync(user, "abc@123").Result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Publisher").Wait();
            }

            ApplicationUser user2 = new ApplicationUser()
            {
                UserName = "user2",
                Email = "user2@localhost",
                FullName = "Martin Scorsese"
            };

            if (userManager.CreateAsync(user2, "abc@123").Result.Succeeded)
            {
                userManager.AddToRoleAsync(user2, "Reader").Wait();
            }
        }
    }
}
