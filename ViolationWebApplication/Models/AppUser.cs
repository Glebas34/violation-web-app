using Microsoft.AspNetCore.Identity;
namespace ViolationWebApplication.Models

{
    public class AppUser : IdentityUser
    {
        public int? OwnerId { get; set; }

    }
}
