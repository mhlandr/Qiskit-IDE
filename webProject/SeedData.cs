using Microsoft.AspNetCore.Identity;
using webProject.Models;

namespace webProject
{

    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminEmail = "admin12@example.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var createAdminResult = await userManager.CreateAsync(adminUser, "Adminn12@123");
                if (createAdminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Log all users and their roles for debugging
            var users = userManager.Users.ToList();
            foreach (var u in users)
            {
                var roles = await userManager.GetRolesAsync(u);
                Console.WriteLine($"User: {u.UserName}, Roles: {string.Join(", ", roles)}");
            }
        }
    }
}
