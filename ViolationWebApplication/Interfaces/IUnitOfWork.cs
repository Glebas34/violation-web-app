using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViolationWebApplication.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IOwnerRepository OwnerRepository { get; }
        IViolationRepository ViolationRepository { get; }
        ICarRepository CarRepository { get; }
        int Complete();
    }
}
