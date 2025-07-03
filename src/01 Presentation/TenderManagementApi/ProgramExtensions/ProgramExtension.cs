using Microsoft.AspNetCore.Identity;
using TenderManagementApi.MiddleWares;
using TenderManagementDAL.Models;

namespace TenderManagementApi.ProgramExtensions
{
    public static class ProgramExtension
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app) => app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        public static async Task SeedData(this IServiceProvider? services)
        {
            await SeedRoles(services!);
            await SeedAdminUser(services!);
        }

        private static async Task SeedRoles(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = ["admin", "vendor"];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedAdminUser(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();

            // Create admin user if not exists
            var adminEmail = "massoud.asgariyan@gmail.com";
            var adminPassword = "Aa@112233445566";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser is null)
            {
                adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            // Ensure user is in the "admin" role
            if (!await userManager.IsInRoleAsync(adminUser, "admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "admin");
            }
        }

    }
}
