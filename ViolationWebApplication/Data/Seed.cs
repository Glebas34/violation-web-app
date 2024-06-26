﻿using Microsoft.AspNetCore.Identity;
using ViolationWebApplication.Data;
using ViolationWebApplication.Models;

public class Seed
{
    public static async Task SeedData(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context.Database.EnsureCreated();
            
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRole.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRole.Admin));

            if (!await roleManager.RoleExistsAsync(UserRole.User))
                await roleManager.CreateAsync(new IdentityRole(UserRole.User));

            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            string adminUserEmail = "admin@gmail.com";

            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
            if (adminUser == null)
            {
                var newAdminUser = new AppUser()
                {
                    UserName = "admin",
                    Email = adminUserEmail,
                    EmailConfirmed = true,
                    FullName = "Александров Александр Александрович"
                };
                newAdminUser.UserName=$"admin_{newAdminUser.Id}";

                await userManager.CreateAsync(newAdminUser, "Coding@1234?");

                await userManager.AddToRoleAsync(newAdminUser, UserRole.Admin);
            }
        }
    }
}