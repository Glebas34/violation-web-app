using Microsoft.AspNetCore.Mvc;
using ViolationWebApplication.Models;
using ViolationWebApplication.Interfaces;
using ViolationWebApplication.ViewModels;

namespace ViolationWebApplication.Controllers
{
    public class ViolationController : Controller
    {
        public const string SessionKeyCarId = "_CarId";
        public const string SessionKeyOwnerId= "_OwnerId";
        private IUnitOfWork _unitOfWork { get; set; }
        private ISession _session { get; set; }
        public ViolationController(IUnitOfWork unitOfWork,ISession session) {
            _unitOfWork = unitOfWork;
            _session = session;
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> AddViolation(ViewModelViolation model) {
            Car? car = await _unitOfWork.CarRepository.GetByNumber(model.CarNumber);
            Violation violation = new Violation();
            violation.TypeOfViolation = model.TypeOfViolation;
            violation.FineFee = model.FineFee;
            if (car != null)
            {
                violation.Car = car;
                violation.CarId = car.Id;
                await _unitOfWork.ViolationRepository.Add(violation);
                _unitOfWork.Complete();
                return Redirect("Home/Index/");
            }
            car = new Car();
            car.CarNumber = model.CarNumber;
            violation.Car = car;
            _session.SetInt32(SessionKeyCarId, car.Id);
            await _unitOfWork.CarRepository.Add(car);
            _unitOfWork.Complete();
            await _unitOfWork.ViolationRepository.Add(violation);
            _unitOfWork.Complete();
            return View("CreateCar");
        }

        public async Task<IActionResult> AddCar(ViewModelCar CarModel)
        {
            Owner? owner = await _unitOfWork.OwnerRepository.GetByDriversLicense(CarModel.DriversLicense);
            Car car = await _unitOfWork.CarRepository.Get(_session.GetInt32(SessionKeyCarId));
            car.Manufacturer = CarModel.Manufacturer;
            car.Model = CarModel.Model;
            if (owner != null)
            {
                car.OwnerId = owner.Id;
                car.Owner = owner;
                _unitOfWork.CarRepository.Update(car);
                _unitOfWork.Complete();
                return Redirect("Home/Index/");
            }
            owner = new Owner();
            owner.DriversLicense = CarModel.DriversLicense;
            car.OwnerId = owner.Id;
            car.Owner = owner;
            _session.SetInt32(SessionKeyOwnerId, owner.Id);
            await _unitOfWork.OwnerRepository.Add(owner);
            _unitOfWork.Complete();
            _unitOfWork.CarRepository.Update(car);
            _unitOfWork.Complete();
            return View("CreateOwner");
        }

        public async Task<IActionResult> AddOwner(ViewModelOwner model)
        {
            Owner owner = await _unitOfWork.OwnerRepository.Get(_session.GetInt32(SessionKeyOwnerId));
            owner.LastName = model.LastName;
            owner.FirstName = model.FirstName;
            owner.Patronymic = model.Patronymic;
            _unitOfWork.OwnerRepository.Update(owner);
            _unitOfWork.Complete();
            return Redirect("Home/Index");
        }
    }
}
