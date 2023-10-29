using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViolationWebApplication.Models

{
    public class AppUser : IdentityUser
    {
        [ForeignKey(nameof(Owner))]
        public int? OwnerId { get; set; }
        public Owner? Owner { get; set; }

    }
}
