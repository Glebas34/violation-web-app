using ViolationWebApplication.Models;

namespace ViolationWebApplication.Interfaces
{
    public interface ICarRepository: IGenericRepository<Car>
    {
        //Поиск автомобиля по его номеру
        Task<Car> GetByNumber(string numberOfCar);
    }
}
