using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Logic;
using Repo;

namespace codecrafters.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly GeneratePassword _generatePassword;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, GeneratePassword generatePassword)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _generatePassword = generatePassword;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Register")]
        public async Task<IActionResult> RegisterPost(UserDTO userDto)
        {
            userDto.Id = Guid.NewGuid();
            var password = _generatePassword.CreatePassword(userDto.Id, DateTime.Now);
            userDto.Password = password;
            var user = new User() { Id = userDto.Id, Created = DateTime.Now, Update = DateTime.Now, UserName = userDto.Username, OneTimePassword = password };
            await _userManager.CreateAsync(user);
            return View();
        }

        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> Login(UserDTO userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.Username);
            var present = DateTime.Now;
            var timeSpan = present.Subtract(user.Created);
            if (!user.PasswordExpired)
            {
                if (timeSpan.TotalSeconds > 30)
                {
                    user.PasswordExpired = true;
                    await _userManager.UpdateAsync(user);
                    ModelState.AddModelError(nameof(UserDTO.Password), "Password has expired");
                    return View();
                }
                user.PasswordExpired = true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError(nameof(UserDTO.Password), "Password has expired");
                return View();
            }
        }

        [HttpPost, ActionName("Generate")]
        public async Task<IActionResult> Generate(UserDTO userDto)
        {
            var time = DateTime.Now;
            var user = await _userManager.FindByNameAsync(userDto.Username);
            var password = _generatePassword.CreatePassword(user.Id, time);
            user.PasswordExpired = false;
            user.Created = time;
            user.OneTimePassword = password;
            await _userManager.UpdateAsync(user);
            ModelState.AddModelError(nameof(UserDTO.Password), password);
            return View("Login");
        }
    }
}
