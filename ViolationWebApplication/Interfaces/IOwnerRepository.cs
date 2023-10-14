using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViolationWebApplication.Models;

namespace ViolationWebApplication.Interfaces
{
    public interface IOwnerRepository: IGenericRepository<Owner>
    {
        Task<Owner> GetByDriversLicense(string? driversLicense);
    }
}
