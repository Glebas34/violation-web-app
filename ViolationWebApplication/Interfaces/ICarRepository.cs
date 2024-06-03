using ViolationWebApplication.Models;

namespace ViolationWebApplication.Interfaces
{
    public interface ICarRepository: IGenericRepository<Car>
    {
        Task<Car> GetByNumber(string numberOfCar);
    }
}
