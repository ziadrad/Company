using System.Security.Policy;
using assignement_3.DAL.Models;
using assignement_3.PL.dto;
using assignement_3.PL.Helpers;
using assignement_3.PL.Interface;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Facebook;

namespace assignement_3.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMailServices _mailServices;
        private readonly ITwilioServices _twilioServices;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailServices mailServices, ITwilioServices twilioServices, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            _mailServices = mailServices;
            _twilioServices = twilioServices;
            this._roleManager = roleManager;
        }
        [HttpGet]

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        // Pass011@
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            var role = await _roleManager.FindByIdAsync(model.Role);

            if (ModelState.IsValid) {

                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user is null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is null)
                    {
                        user = new AppUser
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            PhoneNumber = "+20"+ model.PhonNumber,
                            isAgree = model.IsAgree,

                        };
                        var task = await _userManager.CreateAsync(user, model.Password);
                        if (task.Succeeded)
                        {
                            if (role is not null)
                            {
                                await _userManager.AddToRoleAsync(user, role.Name);
                            }

                            return RedirectToAction("SignIn");
                        }
                        foreach (var erorr in task.Errors)
                        {
                            ModelState.AddModelError("", erorr.Description);
                        }
                    }

                }

                ModelState.AddModelError("", "invalid sign up");

            }
            return View();
        }

        [HttpGet]

        public IActionResult AccessDenied()
        {
            
            return View();
        }

        [HttpGet]

        public async Task <IActionResult> SignIn(string? message = null)
        {
            ViewBag.message = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDtocs model, string? ReturnUrl)
        {
            if (ModelState.IsValid)
            {


                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.Remember, false);

                        if (result.Succeeded)
                        {
                            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }

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
        public IActionResult ForgetPassword(ForgetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.action == "EmailMethod")
                {
                    return RedirectToActionPreserveMethod("sendEmailForgetPassword", "Account", model);
                }
                else
                {
                    return RedirectToActionPreserveMethod("sendSmsForgetPassword", "Account", model);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> sendSmsForgetPassword(ForgetPasswordDto model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);
                    try
                    {
                        _twilioServices.SendSms(
                               new SMS()
                               {

                                   body = url,
                                   To = user.PhoneNumber,

                               });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(key: "", errorMessage: "Invalid Reset Password Operation ! !");
                        return View("ForgetPassword", model);
                    }

                    return RedirectToAction("CheckYourPhone");

                }

            }
            ModelState.AddModelError(key: "", errorMessage: "Invalid Reset Password Operation ! !");
            return View("ForgetPassword", model);
        }

        [HttpGet]
        public IActionResult CheckYourPhone()
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
                    try
                    {
                        _mailServices.sendEmail(
                               new Email()
                               {

                                   body = url,
                                   To = model.Email,
                                   subject = " Reset Password",

                               });
                    }
                    catch (Exception ex) {
                        ModelState.AddModelError(key: "", errorMessage: "Invalid Reset Password Operation ! !");
                        return View("ForgetPassword", model);
                    }

                    return RedirectToAction("CheckYourInbox");

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
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
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
                ModelState.AddModelError("", "Invalid Reset Password Operation");
            }
            return View();
        }


        public IActionResult GoogleLogin()
        {
            var prop = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            };
            return Challenge(prop, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            var cliams = result.Principal.Identities.FirstOrDefault().Claims.Select(
            claim => new
            {
        claim.Type,
            claim.Value,
            claim.Issuer,
            claim.OriginalIssuer
            });
            var current_User = await _userManager.FindByEmailAsync(cliams.ToArray()[4].Value);
            if (current_User is null)
            {
                await _signInManager.SignInAsync(
               new AppUser
               {
                   FirstName = cliams.ToArray()[2].Value,
                   UserName = cliams.ToArray()[1].Value,
                   Email = cliams.ToArray()[4].Value
               }, false);
            }
            else
            {
                
                return RedirectToAction("SignIn", "Account",new { message = "user already exists"});
            }



            return RedirectToAction("Index", "Home");


        }

        public IActionResult FacebookLogin()
        {
            var prop = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("FacebookResponse")
            };
            return Challenge(prop, FacebookDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> FacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(FacebookDefaults.AuthenticationScheme);
            var cliams = result.Principal.Identities.FirstOrDefault().Claims.Select(
            claim => new
            {
                claim.Type,
                claim.Value,
                claim.Issuer,
                claim.OriginalIssuer
            });
            var current_User = await _userManager.FindByEmailAsync(cliams.ToArray()[4].Value);
            if (current_User is null)
            {
                await _signInManager.SignInAsync(
               new AppUser
               {
                   FirstName = cliams.ToArray()[2].Value,
                   UserName = cliams.ToArray()[1].Value,
                   Email = cliams.ToArray()[4].Value
               }, false);
            }
            else
            {

                return RedirectToAction("SignIn", "Account", new { message = "user already exists" });
            }



            return RedirectToAction("Index", "Home");


        }
    }
}
