using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TenderManagementApi.MiddleWares;
using TenderManagementDAL.Contexts;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;
using TenderManagementDAL.Repositories.ReadRepositories;
using TenderManagementDAL.UnitOfWorks;

namespace TenderManagementApi.ProgramExtensions
{
    public static class ProgramExtension
    {
        public static void AddSqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TenderManagementDbContext>(options => options.UseSqlServer(connectionString,
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(15),
                        errorNumbersToAdd: null);
                }
            ));
            var serviceProvider = services.BuildServiceProvider();
            var dataContext = serviceProvider!.GetService<TenderManagementDbContext>();

            //for repositories
            services.AddSingleton<IEfDataContext>(dataContext!);
        }

        public static void AddUnitOfWorks(this IServiceCollection services)
        {
            services.AddScoped<ITenderManagementUnitOfWork, TenderManagementUnitOfWork>();
        }

        public static void AddDapperConnectionAndRepos(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

            services.AddScoped<IReadableBidRepository>(sp =>
            {
                var connection = sp.GetRequiredService<IDbConnection>();
                return new ReadableBidRepository(connection, "Bids");
            });

            services.AddScoped<IReadableCategoryRepository>(sp =>
            {
                var connection = sp.GetRequiredService<IDbConnection>();
                return new ReadableCategoryRepository(connection, "Categories");
            });

            services.AddScoped<IReadableTenderRepository>(sp =>
            {
                var connection = sp.GetRequiredService<IDbConnection>();
                return new ReadableTenderRepository(connection, "Tenders");
            });

            services.AddScoped<IReadableVendorRepository>(sp =>
            {
                var connection = sp.GetRequiredService<IDbConnection>();
                return new ReadableVendorRepository(connection, "Vendors");
            });

            services.AddScoped<IReadableStatusRepository>(sp =>
            {
                var connection = sp.GetRequiredService<IDbConnection>();
                return new ReadableStatusRepository(connection, "Statuses");
            });
        }

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
