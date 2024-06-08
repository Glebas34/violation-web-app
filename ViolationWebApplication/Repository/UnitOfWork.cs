using ViolationWebApplication.Data;
using ViolationWebApplication.Interfaces;

namespace ViolationWebApplication.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IViolationRepository ViolationRepository { get; }

        public ICarRepository CarRepository { get; }
        public UnitOfWork(AppDbContext context, IViolationRepository violationRepository,ICarRepository carRepository) 
        {
            _context = context;
            ViolationRepository = violationRepository;
            CarRepository = carRepository;
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
           _context.Dispose();
        }
    }
}
