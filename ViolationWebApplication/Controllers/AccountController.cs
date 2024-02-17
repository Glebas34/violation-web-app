﻿using Microsoft.AspNetCore.Mvc;
using ViolationWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using ViolationWebApplication.ViewModels;
using ViolationWebApplication.Data;
using ViolationWebApplication.Interfaces;
using Microsoft.AspNetCore.Authorization;

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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(ViewModelLogin model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

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
                TempData["Error"] = "Неверные реквизиты. Попробуйте снова.";
                return View(model);
            }
            TempData["Error"] = "Неверные реквизиты. Попробуйте снова.";
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

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
            user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                TempData["Error"] = "Имя пользователя занято.";
                return View(model);
            }
            var owner = await _unitOfWork.OwnerRepository.GetByDriversLicense(model.DriversLicense);
            if (owner == null)
            {
                var newOwner = new Owner { FirstName = model.FirstName, LastName = model.LastName, Patronymic = model.Patronymic, DriversLicense = model.DriversLicense};
                owner = newOwner;
                await _unitOfWork.OwnerRepository.Add(owner);
            }
            var newUser = new AppUser { UserName = model.UserName, Email = model.Email, OwnerId = owner.Id, Owner = owner};
            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);
            if (newUserResponse.Succeeded) 
            {
                await _userManager.AddToRoleAsync(newUser, UserRole.User);
                _unitOfWork.Complete();
                await _signInManager.SignInAsync(newUser, isPersistent: false);
            }
            return RedirectToAction("Index","Home");
        }

        [Authorize]
        [HttpGet]
         public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
