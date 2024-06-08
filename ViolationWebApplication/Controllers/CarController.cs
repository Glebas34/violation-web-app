using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViolationWebApplication.Interfaces;
using ViolationWebApplication.Models;
using ViolationWebApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using ViolationWebApplication.Service;

namespace ViolationWebApplication.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public CarController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }

        //Метод для отображения страницы Car/Add(Добавление автомобиля)
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //Метод для обработки введённых данных на странице Car/Add(Добавление автомобиля)
        [HttpPost]
        public async Task<IActionResult> Add(ViewModelCar carModel)
        {
            if(ModelState.IsValid)
            {
                var car = await _unitOfWork.CarRepository.GetByNumber(carModel.CarNumber);
                
                if(car != null)
                {
                    TempData["Error"] = "Автомобиль с таким номером уже существует";
                    return View(carModel);
                }

                car = new Car
                {
                    CarNumber=carModel.CarNumber,
                    OwnerFullName = carModel.FullName,
                    Manufacturer = carModel.Manufacturer,
                    OwnerPassport = carModel.Passport,
                    Model = carModel.Model

                };

                var user = await _userManager.FindByPassportAsync(carModel.Passport);

                if(user is not null)
                {
                    car.UserId = user.Id;
                    car.AppUser = user;
                }

                await _unitOfWork.CarRepository.Add(car);
                await _unitOfWork.Complete();

                return RedirectToAction("Index","Home");
            }
            return View();
        }

        //Метод для отображения страницы Car/Update(Редактирование данные об автомобиле)
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var car = await _unitOfWork.CarRepository.Get(id);

            var vm = new ViewModelCar
            {
                CarNumber = car.CarNumber,
                Manufacturer = car.Manufacturer,
                Model = car.Model,
                Passport = car.OwnerPassport,
                FullName = car.OwnerFullName,
                Id = car.Id
            };

            return View(vm);
        }

        //Метод для обработки введённых данных на странице Car/Update/{id}(Редактирование данные об автомобиле)
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Update(ViewModelCar carModel)
        {
            if(ModelState.IsValid)
            {
                var car = await _unitOfWork.CarRepository.GetByNumber(carModel.CarNumber);
                
                if(car != null && car.Id != carModel.Id)
                {
                    TempData["Error"] = "Автомобиль с таким номером уже существует";

                    return View(carModel);
                }

                car = await _unitOfWork.CarRepository.Get(carModel.Id);
                
                car.CarNumber = carModel.CarNumber;
                car.Manufacturer = carModel.Manufacturer;
                car.Model = carModel.Model;
                car.OwnerFullName = carModel.FullName;
                car.OwnerPassport = carModel.Passport;

                _unitOfWork.CarRepository.Update(car);
                await _unitOfWork.Complete();

                return RedirectToAction("ShowAll", "Car");
            }

            return View(carModel);
        }

        //Метод для отображения страницы Car/ShowAll(Вывод данных об автомобилях)
        [HttpGet]
        public IActionResult ShowAll()
        {
            TempData["Error"] = null;
            
            return View();
        }

        //Метод для удаления автомобиля
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.CarRepository.Delete(id);
            await _unitOfWork.Complete();

            return RedirectToAction("Index", "Home");
        }
    }
}