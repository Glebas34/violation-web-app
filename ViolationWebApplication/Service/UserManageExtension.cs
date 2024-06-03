using Microsoft.AspNetCore.Identity;
using ViolationWebApplication.Interfaces;
using ViolationWebApplication.Repository;
using ViolationWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ViolationWebApplication.Service
{
    public static class UserManagerExtensions
    {

        public static async Task<AppUser> FindByPassportAsync(this UserManager<AppUser> manager, string passport)
        {
            return await manager.Users.FirstOrDefaultAsync(u => u.Passport==passport);
        }
    }
}