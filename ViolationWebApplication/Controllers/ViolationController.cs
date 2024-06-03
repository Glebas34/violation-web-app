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
        public const string SessionKeyViolation = "_Violation";
        private IUnitOfWork _unitOfWork { get; set; }
        private ISession _session { get; set; }

        public ViolationController(IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _session = httpContextAccessor.HttpContext.Session;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Add(ViewModelViolation model) 
        {
            if (ModelState.IsValid)
            {
                var violation = new Violation
                {
                    TypeOfViolation = model.TypeOfViolation,
                    FineFee = model.FineFee
                };

                var car = await _unitOfWork.CarRepository.GetByNumber(model.CarNumber);

                if(car != null)
                {
                    violation.Car = car;
                    violation.CarId = car.Id;

                    await _unitOfWork.ViolationRepository.Add(violation);
                    _unitOfWork.Complete();

                    return RedirectToAction("Index","Home");
                }

                TempData["Error"] = "Автомобиля с таким номеров не существует";
            }

            return View(model);
        }

        [Authorize]
        public IActionResult ShowAll()
        {
            TempData["Error"] = null;

            return View();
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> PayFine(int id) 
        {
            ViewData["CarId"]=id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.ViolationRepository.Delete(id);
            _unitOfWork.Complete();

            return RedirectToAction("ShowAll", "Violation");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var violation = await _unitOfWork.ViolationRepository.Get(id);

            await _unitOfWork.ViolationRepository.ExplicitLoading(violation, "Car");
            _session.Set<Violation>(SessionKeyViolation, violation);
            
            var car = violation.Car;

            var vm = new ViewModelViolation
            {
                TypeOfViolation = violation.TypeOfViolation,
                CarNumber = car.CarNumber,
                FineFee = violation.FineFee
            };

            return View("Update",vm);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Update(ViewModelViolation model)
        {
            if(ModelState.IsValid)
            {
                var car = await _unitOfWork.CarRepository.GetByNumber(model.CarNumber);

                if(car is null)
                {
                    TempData["Error"] = "Автомобиля с таким номеров не существует";

                    return View(model);
                }

                var violation = _session.Get<Violation>(SessionKeyViolation);

                violation.TypeOfViolation = model.TypeOfViolation;
                violation.FineFee = model.FineFee;
                violation.CarId = car.Id;
                violation.Car = car;

                _unitOfWork.ViolationRepository.Update(violation);
                _unitOfWork.Complete();

                return RedirectToAction("ShowAll", "Violation");
            }

            return View(model);
        }
    }
}
