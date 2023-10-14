using Microsoft.AspNetCore.Mvc;
using ViolationWebApplication.Interfaces;
using ViolationWebApplication.Models;
using ViolationWebApplication.ViewModels;

namespace ViolationWebApplication.Controllers
{
    [Route("Car")]
    public class CarController : Controller
    {
        IUnitOfWork _unitOfWork { get; set; }
        public CarController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ViewModelCar CarModel)
        {
            Owner? owner = await _unitOfWork.OwnerRepository.GetByDriversLicense(CarModel.DriversLicense);
            Car car = await _unitOfWork.CarRepository.GetByNumber(CarModel.CarNumber);
            car.Manufacturer = CarModel.Manufacturer;
            car.Model = CarModel.Model;
            car.CarNumber = CarModel.CarNumber;
            if (owner != null)
            {
                car.OwnerId= owner.Id;
                car.Owner = owner;
                _unitOfWork.CarRepository.Update(car);
                _unitOfWork.Complete();
                return Redirect("Home/Index/");
            }
            owner = new Owner();
            owner.DriversLicense = CarModel.DriversLicense;
            car.OwnerId = owner.Id;
            car.Owner = owner;
            await _unitOfWork.OwnerRepository.Add(owner);
            _unitOfWork.Complete();
            _unitOfWork.CarRepository.Update(car);
            _unitOfWork.Complete();
            return Redirect($"Owner/Create");
        }
    }
}
