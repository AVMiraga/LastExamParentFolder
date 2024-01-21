using ExamTask.Business.Services.Interfaces;
using ExamTask.Business.ViewModels.AccountVms;
using ExamTask.Core.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamTask.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(IAccountService service, SignInManager<AppUser> signInManager)
        {
            _service = service;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            LoginVmValidator validator = new LoginVmValidator();
            ValidationResult result = await validator.ValidateAsync(vm);

            if(!result.IsValid)
            {
                ModelState.Clear();

                result.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View();
            }

            var res =  await _service.CheckCredentialsAsync(vm);

            if(!res.Success || res.AppUser is null)
            {
                ModelState.AddModelError("", "Username/Email or Password is wrong");

                return View();
            }

            await _signInManager.SignInAsync(res.AppUser, true);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm, string? returnUrl)
        {
            RegisterVmValidator validator = new RegisterVmValidator();
            ValidationResult result = await validator.ValidateAsync(vm);

            if(!result.IsValid)
            {
                ModelState.Clear();

                result.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View();
            }

            var res = await _service.RegisterAsync(vm);

            if(!res.IdentityResult.Succeeded || res.AppUser is null)
            {
                res.IdentityResult.Errors.ToList().ForEach(x => ModelState.AddModelError("", x.Description));

                return View();
            }

            await _signInManager.SignInAsync(res.AppUser, false);

            if(returnUrl is not null && (!returnUrl.Contains("Register") || !returnUrl.Contains("Login")))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRoles()
        {
            await _service.CreateRolesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
