using Microsoft.EntityFrameworkCore;
using ViolationWebApplication.Data;
using ViolationWebApplication.Interfaces;
using ViolationWebApplication.Models;

namespace ViolationWebApplication.Repository
{
    class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository(AppDbContext context) : base(context)
        {

        }

        async Task<Car> ICarRepository.GetByNumber(string numberOfCar)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.CarNumber == numberOfCar);
        }
    }
}
