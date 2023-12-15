using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ViolationWebApplication.Data;
using ViolationWebApplication.Interfaces;
using ViolationWebApplication.Models;

namespace ViolationWebApplication.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IOwnerRepository OwnerRepository { get; }

        public IViolationRepository ViolationRepository { get; }

        public ICarRepository CarRepository { get; }
        public UnitOfWork(AppDbContext context,IOwnerRepository ownerRepository,IViolationRepository violationRepository,ICarRepository carRepository) 
        {
            _context = context;
            OwnerRepository = ownerRepository;
            ViolationRepository = violationRepository;
            CarRepository = carRepository;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
           _context.Dispose();
        }
    }
}
