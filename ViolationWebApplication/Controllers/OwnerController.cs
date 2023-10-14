using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViolationWebApplication.Interfaces;
using ViolationWebApplication.Models;
using ViolationWebApplication.Repository;
using ViolationWebApplication.ViewModels;

namespace ViolationWebApplication.Controllers
{
    [Route("Owner")]
    public class OwnerController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public OwnerController(IUnitOfWork unitOfWork, AppDbContext appDbContext) {
            _unitOfWork = unitOfWork; 
        }
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ViewModelOwner model)
        {
            List<Car> cars = (await _unitOfWork.CarRepository.GetAll()).ToList();
            await _unitOfWork.CarRepository.ExplicitLoadingCollection(cars,"Owner");
            Car car = cars.Where(e => e.Owner.DriversLicense == model.DriversLicense).First();
            Owner owner = await _unitOfWork.OwnerRepository.Get(car.OwnerId);
            owner.DriversLicense= model.DriversLicense;
            owner.LastName= model.LastName;
            owner.FirstName= model.FirstName;
            owner.Patronymic = model.Patronymic;
            _unitOfWork.OwnerRepository.Update(owner);
            _unitOfWork.Complete();
            return Redirect("Home/Index");
        }
    }
}
