using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViolationWebApplication.Models;

namespace ViolationWebApplication.Interfaces
{
    public interface ICarRepository: IGenericRepository<Car>
    {
        Task<Car> GetByNumber(string? numberOfCar);
    }
}
