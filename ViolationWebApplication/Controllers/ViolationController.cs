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

        //Метод для отображения страницы Violation/Add(Добавление добавление нарушения)
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //Метод для обработки введённых данных на странице Violation/Add(Добавление нарушения)
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
                    await _unitOfWork.Complete();

                    return RedirectToAction("Index","Home");
                }

                TempData["Error"] = "Автомобиля с таким номеров не существует";
            }

            return View(model);
        }

        //Метод для отображения страницы Violation/ShowAll(Вывод данных о нарушениях)
        [Authorize]
        public IActionResult ShowAll()
        {
            TempData["Error"] = null;

            return View();
        }

        //Метод для отображения страницы Violation/PayFine/{id}(Оплата штрафа)
        [Authorize(Roles = "user")]
        [HttpGet]
        public IActionResult PayFine(int id) 
        {
            ViewData["CarId"]=id;

            return View();
        }

        //Метод для удаления нарушения
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.ViolationRepository.Delete(id);
            await _unitOfWork.Complete();

            return RedirectToAction("ShowAll", "Violation");
        }

        //Метод для отображения страницы Violation/Update/{id}(Редактирование данных о нарушении)
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

            return View(vm);
        }

        //Метод для обработки введённых данных на странице Violation/Update/{id}(Редактирование данных о нарушении)
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
                await _unitOfWork.Complete();

                return RedirectToAction("ShowAll", "Violation");
            }

            return View(model);
        }
    }
}
