using Microsoft.AspNetCore.Mvc;
using ViolationWebApplication.Models;
using ViolationWebApplication.Interfaces;
using ViolationWebApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ViolationWebApplication.Service;

namespace ViolationWebApplication.Controllers
{
    [Authorize]
    public class ViolationController : Controller
    {
        public const string SessionKeyCar = "_Car";
        public const string SessionKeyOwner = "_Owner";
        public const string SessionKeyViolation = "_Violation";
        private IUnitOfWork _unitOfWork { get; set; }
        private ISession _session { get; set; }
        private IViolationService _violationService { get; set; }

        public ViolationController(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor, IViolationService violationService) {
            _unitOfWork = unitOfWork;
            _session = httpContextAccessor.HttpContext.Session;
            _violationService = violationService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddViolation()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddViolation(ViewModelViolation model) {
            if (ModelState.IsValid)
            {
                Violation violation = new Violation();

                violation.TypeOfViolation = model.TypeOfViolation;
                violation.FineFee = model.FineFee;

                Car? car = await _unitOfWork.CarRepository.GetByNumber(model.CarNumber);
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

                _session.Set(SessionKeyCar, car);
                _session.Set(SessionKeyViolation, violation);

                return View("AddCar");
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddCar(ViewModelCar CarModel)
        {
            if (ModelState.IsValid)
            {
                Car car = _session.Get<Car>(SessionKeyCar);

                car.Manufacturer = CarModel.Manufacturer;
                car.Model = CarModel.Model;

                Owner? owner = await _unitOfWork.OwnerRepository.GetByDriversLicense(CarModel.DriversLicense);
                if (owner != null)
                {
                    Violation violation = _session.Get<Violation>(SessionKeyViolation);
                    car.OwnerId = owner.Id;
                    car.Owner = owner;
                    violation.Car = car;

                    await _unitOfWork.ViolationRepository.Add(violation);
                    await _unitOfWork.CarRepository.Add(car);
                    _unitOfWork.Complete();

                    return RedirectToAction("Index", "Home");
                }
                owner = new Owner();

                owner.DriversLicense = CarModel.DriversLicense;
                car.OwnerId = owner.Id;

                _session.Set(SessionKeyCar, car);
                _session.Set(SessionKeyOwner, owner);

                return View("AddOwner");
            }
           return View(CarModel);
        }

        [Authorize(Roles = "admin")]
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
                car.Owner = owner;
                violation.Car = car;

                await _unitOfWork.ViolationRepository.Add(violation);
                await _unitOfWork.CarRepository.Add(car);
                await _unitOfWork.OwnerRepository.Add(owner);
                _unitOfWork.Complete();

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [Authorize(Roles = "user")]
        public async Task<IActionResult> ShowAllViolations()
        {
            return View();
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> PayFine(int id) {
            Violation violation = await _unitOfWork.ViolationRepository.Get(id);
            _session.Set(SessionKeyViolation, violation);
            return View();
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> PayFine()
        {
            Violation violation = _session.Get<Violation>(SessionKeyViolation);
            await _violationService.DeleteViolation(violation);
            return RedirectToAction("ShowAllViolations", "Violation");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteViolation(int id)
        {
            Violation violation = await _unitOfWork.ViolationRepository.Get(id);
            await _violationService.DeleteViolation(violation);
            return RedirectToAction("ShowAllViolations", "Violation");
        }
    }
}
