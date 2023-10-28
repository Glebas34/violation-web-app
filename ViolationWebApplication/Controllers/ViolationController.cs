using Microsoft.AspNetCore.Mvc;
using ViolationWebApplication.Models;
using ViolationWebApplication.Interfaces;
using ViolationWebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ViolationWebApplication.Controllers
{
    public class ViolationController : Controller
    {
        public const string SessionKeyCarNumber = "_CarNumber";
        public const string SessionKeyDriversLicense = "_DriversLicense";
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
                    return Redirect("~/Home/Index/");
                }
                car = new Car();
                car.CarNumber = model.CarNumber;
                violation.Car = car;
                _session.SetString(SessionKeyCarNumber, car.CarNumber);
                await _unitOfWork.CarRepository.Add(car);
                _unitOfWork.Complete();
                await _unitOfWork.ViolationRepository.Add(violation);
                _unitOfWork.Complete();
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
                string carNumber = _session.GetString(SessionKeyCarNumber);
                Car car = await _unitOfWork.CarRepository.GetByNumber(carNumber);
                car.Manufacturer = CarModel.Manufacturer;
                car.Model = CarModel.Model;
                if (owner != null)
                {
                    car.OwnerId = owner.Id;
                    car.Owner = owner;
                    _unitOfWork.CarRepository.Update(car);
                    _unitOfWork.Complete();
                    return Redirect("~/Home/Index");
                }
                owner = new Owner();
                owner.DriversLicense = CarModel.DriversLicense;
                car.OwnerId = owner.Id;
                car.Owner = owner;
                _session.SetString(SessionKeyDriversLicense, owner.DriversLicense);
                await _unitOfWork.OwnerRepository.Add(owner);
                _unitOfWork.Complete();
                _unitOfWork.CarRepository.Update(car);
                _unitOfWork.Complete();
                return View("AddOwner");
            }
           return View(CarModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddOwner(ViewModelOwner model)
        {
            if (ModelState.IsValid)
            {
                Owner owner = await _unitOfWork.OwnerRepository.GetByDriversLicense(_session.GetString(SessionKeyDriversLicense));
                owner.LastName = model.LastName;
                owner.FirstName = model.FirstName;
                owner.Patronymic = model.Patronymic;
                _unitOfWork.OwnerRepository.Update(owner);
                _unitOfWork.Complete();
                return Redirect("~/Home/Index");
            }
            return View(model);
        }
    }
}
