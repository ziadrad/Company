using assignement_3.DAL.Models;
using assignement_3.PL.dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace assignement_3.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        [HttpGet]      
        
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        // P@ssWørd
        public async Task< IActionResult> SignUp( SignUpDto model)
        {
            if (ModelState.IsValid) {

                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user is null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if(user is null)
                    {
                         user = new AppUser
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            isAgree = model.IsAgree,

                        };
                        var task = await _userManager.CreateAsync(user, model.Password);
                        if (task.Succeeded)
                        {
                            return RedirectToAction("SignIn");
                        }
                        foreach (var erorr in task.Errors)
                        {
                            ModelState.AddModelError("", erorr.Description);
                        }
                    }
                    
                }

                ModelState.AddModelError("", "invalid sign in");

            }
            return View();
        }


        [HttpGet]

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDtocs model)
        {
            if (ModelState.IsValid)
            {

               
                   var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is not null)
                    {
                 var flag = await   _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.Remember,false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                    }

                

                ModelState.AddModelError("", "invalid sign in");

            }
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
    }
}
