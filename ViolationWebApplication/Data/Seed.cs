using Microsoft.AspNetCore.Identity;
using ViolationWebApplication.Data;
using ViolationWebApplication.Models;

public class Seed
{
    public static void SeedData(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            context.Database.EnsureCreated();

            if (!context.Owners.Any())
            {
                context.Owners.Add(new Owner { FirstName = "Иван", LastName = "Иванов", Patronymic = "Петрович", DriversLicense = "2281337322" });
                context.SaveChanges();
            }
        }
    }
    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            if (!await roleManager.RoleExistsAsync(UserRole.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
            if (!await roleManager.RoleExistsAsync(UserRole.User))
                await roleManager.CreateAsync(new IdentityRole(UserRole.User));

            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            string adminUserEmail = "kuimov.gleb30122002@gmail.com";

            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
            if (adminUser == null)
            {
                var newAdminUser = new AppUser()
                {
                    NormalizedUserName = "Glebas34",
                    Email = adminUserEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                await userManager.AddToRoleAsync(newAdminUser, UserRole.Admin);
            }

            adminUserEmail = "pochta4e6ypeka@gmail.com";

            adminUser = await userManager.FindByEmailAsync(adminUserEmail);
            if (adminUser == null)
            {
                var newAdminUser = new AppUser()
                {
                    NormalizedUserName = "4e6ypek",
                    Email = adminUserEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                await userManager.AddToRoleAsync(newAdminUser, UserRole.Admin);
            }

            string userEmail = "pochta2023@gmail.com";

            var user = await userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                var newUser = new AppUser()
                {
                    UserName = "userochek",
                    Email = userEmail,
                    EmailConfirmed = true,
                    OwnerId = 1,
                    Owner = context.Owners.Find(1)
                };
                await userManager.CreateAsync(newUser, "Coding@1234?");
                await userManager.AddToRoleAsync(newUser, UserRole.User);
            }
        }
    }
}