using Microsoft.AspNetCore.Mvc;
using ViolationWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using ViolationWebApplication.ViewModels;
using ViolationWebApplication.Data;
using ViolationWebApplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ViolationWebApplication.Service;

namespace ViolationWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

       public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        //Метод для отображения страницы Account/Login(Войти)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        //Метод для обработки введённых данных на странице Account/Login(Войти)
        [HttpPost]
        public async Task<IActionResult> Login(ViewModelLogin model)
        {
            if (ModelState.IsValid) 
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                { 
                    var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);

                    if (passwordCheck)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                        if (result.Succeeded) 
                        {
                            return RedirectToAction("Index","Home");
                        }
                    }
                }
            }

            TempData["Error"] = "Неверные реквизиты. Попробуйте снова.";

            return View(model);
        }

        //Метод для отображения страницы Account/Register(Регистрация)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //Метод для обработки введённых данных на странице Account/Register(Регистрация)
        [HttpPost]
        public async Task<IActionResult> Register(ViewModelRegister model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null) 
            {
                TempData["Error"] = "Email занят.";
                return View(model);
            }

            user = await _userManager.FindByPassportAsync(model.Passport);

            if (user != null)
            {
                TempData["Error"] = "Пользователь с таким паспортом уже зарегистрирован.";
                return View(model);
            }

            var newUser = new AppUser {Email = model.Email, FullName = $"{model.LastName} {model.FirstName} {model.Patronymic}", Passport = model.Passport};
            newUser.UserName = $"user_{newUser.Id}";

            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);

            if (newUserResponse.Succeeded) 
            {
                var cars = (await _unitOfWork.CarRepository.GetAll()).Where(c => c.OwnerPassport==model.Passport).ToList();
                
                foreach(var car in cars)
                {
                    car.UserId = newUser.Id;
                    car.AppUser = newUser;
                    _unitOfWork.CarRepository.Update(car);
                }

                await _unitOfWork.Complete();

                await _userManager.AddToRoleAsync(newUser, UserRole.User);
                await _signInManager.SignInAsync(newUser, isPersistent: false);
            }

            return RedirectToAction("Index","Home");
        }

        //Метод для обработки выхода из аккаунта
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        //Метод для отображения страницы Account/Profile(Профиль)
        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        //Метод для отображения страницы Account/AddAdmin(Добавить админа)
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }

        //Метод для обработки введённых данных на странице Account/AddAdmin
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddAdmin(ViewModelAdmin model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null) 
            {
                TempData["Error"] = "Email занят.";

                return View(model);
            }

            var newUser = new AppUser {Email = model.Email, FullName = $"{model.LastName} {model.FirstName} {model.Patronymic}"};
            newUser.UserName = $"admin_{newUser.Id}";
            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);

            if (newUserResponse.Succeeded) 
            {
                await _userManager.AddToRoleAsync(newUser, UserRole.Admin);
            }

            return RedirectToAction("Index","Home");
        }
    }
}
