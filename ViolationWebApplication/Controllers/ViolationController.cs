using Microsoft.AspNetCore.Mvc;
using ViolationWebApplication.Models;
using ViolationWebApplication.Interfaces;
using ViolationWebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ViolationWebApplication.Service;

namespace ViolationWebApplication.Controllers
{
    public class ViolationController : Controller
    {
        public const string SessionKeyCar = "_Car";
        public const string SessionKeyOwner = "_Owner";
        public const string SessionKeyViolation = "_Violation";
        private IUnitOfWork _unitOfWork { get; set; }
        private ISession _session { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ViolationController(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor) {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddViolation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddViolation(ViewModelViolation model) {
            if (ModelState.IsValid)
            {
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
                    return RedirectToAction("Index","Home");
                }
                car = new Car();
                car.CarNumber = model.CarNumber;
                violation.Car = car;
                _session.Set(SessionKeyCar, car);
                _session.Set(SessionKeyViolation, violation);
                return View("AddCar");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddCar(ViewModelCar CarModel)
        {
            if (ModelState.IsValid)
            {
                Owner? owner = await _unitOfWork.OwnerRepository.GetByDriversLicense(CarModel.DriversLicense);
                Car car = _session.Get<Car>(SessionKeyCar);
                car.Manufacturer = CarModel.Manufacturer;
                car.Model = CarModel.Model;
                if (owner != null)
                {
                    Violation violation = _session.Get<Violation>(SessionKeyViolation);
                    car.OwnerId = owner.Id;
                    car.Owner = owner;
                    await _unitOfWork.ViolationRepository.Add(violation);
                    await _unitOfWork.CarRepository.Add(car);
                    _unitOfWork.Complete();
                    return RedirectToAction("Index", "Home");
                }
                owner = new Owner();
                owner.DriversLicense = CarModel.DriversLicense;
                car.OwnerId = owner.Id;
                car.Owner = owner;
                _session.Set(SessionKeyCar, car);
                _session.Set(SessionKeyOwner, owner);
                return View("AddOwner");
            }
           return View(CarModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddOwner(ViewModelOwner model)
        {
            if (ModelState.IsValid)
            {
                Owner owner = _session.Get<Owner>(SessionKeyOwner);
                Car car = _session.Get<Car>(SessionKeyCar);
                Violation violation = _session.Get<Violation>(SessionKeyViolation);
                owner.LastName = model.LastName;
                owner.FirstName = model.FirstName;
                owner.Patronymic = model.Patronymic;
                await _unitOfWork.ViolationRepository.Add(violation);
                await _unitOfWork.CarRepository.Add(car);
                await _unitOfWork.OwnerRepository.Add(owner);
                _unitOfWork.Complete();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
