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
    class ViolationRepository: GenericRepository<Violation>, IViolationRepository
    {
        public ViolationRepository(AppDbContext context) : base(context)
        {

        }
    }
}
