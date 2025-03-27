using System.Security.Policy;
using assignement_3.DAL.Models;
using assignement_3.PL.dto;
using assignement_3.PL.Helpers;
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

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> sendEmailForgetPassword(ForgetPasswordDto model) {

            if (ModelState.IsValid) {
              var user = await _userManager.FindByEmailAsync(model.Email);
                
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);
                  var result =  EmailSettings.sendEmail(
                        new Email() {
                            
                            body = url,
                            To = model.Email, 
                            subject = " Reset Password" ,
                        
                        });

                    if (result)
                    {
                       return RedirectToAction("CheckYourInbox");
                    }
                }

            }
            ModelState.AddModelError(key: "", errorMessage: "Invalid Reset Password Operation ! !");
            return View("ForgetPassword", model);
        }


        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"]=email;
            TempData["token"]=token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (ModelState.IsValid) {
                var email = TempData[key: "email"] as string;
                var token = TempData[key: "token"] as string;

                if (email is null || token is null) return BadRequest(error: "Invalid Operations");
                var user = await _userManager.FindByEmailAsync(email);
                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                    if (result.Succeeded) return RedirectToAction(actionName: "SignIn");
                }
                ModelState.AddModelError("","Invalid Reset Password Operation");
            }
            return View();
        }
    }
}
