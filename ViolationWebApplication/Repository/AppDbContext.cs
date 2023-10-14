using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ViolationWebApplication.Models;

namespace ViolationWebApplication.Repository;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }

    public DbSet<Owner> Owners { get; set; }

    public DbSet<Violation> Violations { get; set; }

    
}
