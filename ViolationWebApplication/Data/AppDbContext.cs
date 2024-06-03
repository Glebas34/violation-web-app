using Microsoft.EntityFrameworkCore;
using ViolationWebApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ViolationWebApplication.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Violation> Violations { get; set; }
    }
}
