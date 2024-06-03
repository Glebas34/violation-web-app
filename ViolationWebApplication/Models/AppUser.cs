using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViolationWebApplication.Models

{
    public class AppUser : IdentityUser
    {
        public string? Passport { get; set; }
        public string FullName { get; set; }
        public ICollection<Car>? Cars{ get; set; }
    }
}
