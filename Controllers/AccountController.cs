using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Security.Claims;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly appdbcontext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, appdbcontext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IActionResult Users()
        {
            var users=_context.Users.ToList();

            return View(users);
        }
        [HttpGet]
        public IActionResult Login()=>View(new LoginViewModel());

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            

            string username = new EmailAddressAttribute().IsValid(login.Email)
                ? _userManager.FindByEmailAsync(login.Email).Result.UserName : login.Email;

            var result = await _signInManager.PasswordSignInAsync(username, login.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Movies");
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(login);

        }
        [HttpGet]
        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) return View(register);

            var user = await _userManager.FindByEmailAsync(register.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(register);
            }

            var newuser = new ApplicationUser()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName=new MailAddress(register.Email).User,
            };
            var newUserResponse = await _userManager.CreateAsync(newuser, register.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newuser, UserRoles.User);
                return View("RegisterCompleted");
            }
            return View(register);
           
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }
}
