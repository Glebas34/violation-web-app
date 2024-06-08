using ViolationWebApplication.Data;
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
