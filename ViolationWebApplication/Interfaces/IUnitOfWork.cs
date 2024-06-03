using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViolationWebApplication.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IViolationRepository ViolationRepository { get; }
        ICarRepository CarRepository { get; }
        int Complete();
    }
}
