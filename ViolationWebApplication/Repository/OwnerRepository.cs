using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViolationWebApplication.Interfaces;
using ViolationWebApplication.Models;

namespace ViolationWebApplication.Repository
{
    class OwnerRepository: GenericRepository<Owner>,IOwnerRepository
    {
        public OwnerRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Owner> GetByDriversLicense(string driversLicense)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.DriversLicense == driversLicense);
        }
    }
}
