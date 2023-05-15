using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using TeacherOnline.BLL.Services;
using TeacherOnline.Models;
using TeacherOnline.DAL.Entities;

namespace TeacherOnline.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        UserService _user;
        ProfileService _profile;
        AuthService _auth;

        public UsersController(ILogger<HomeController> logger, UserService user, ProfileService profile, AuthService auth)
        {
            _logger = logger;
            _user = user;
            _profile = profile;
            _auth = auth;
        }

        //-------------------------------------------------------------------------------------------
        //Method Get

        [HttpGet]
        public IActionResult Autorization()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "Study")] //тут же добавиться только авторизированным пользователям
        public  IActionResult UserProfile()
        {
            var tempuser = _user.Get(Convert.ToInt32(HttpContext.Request.Cookies["Id"]));
            if (tempuser is null) return RedirectToAction("CreateProfile");
            return View(tempuser);
        }

        [HttpGet]
        [Authorize(Policy = "Study")]
        public IActionResult CreateProfile()
        {
            Profile st = new Profile() { Id = Convert.ToInt32(HttpContext.Request.Cookies["Id"]) };
            return View(st);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult UpdateUser(int id)
        {
            var tempuser = _user.Get(id);
            return View(tempuser);
        }

        //Method Post

        [HttpPost]
        public async Task<IResult> Autorization(User users, string? returnUrl)
        {
            await HttpContext.SignInAsync(_auth.LogIn(users));
            return Results.Redirect(returnUrl);
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            _user.Create(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult UpdateProfile(Profile newProfile)
        {
            _profile.Update(newProfile, null);
            //естественно перепревкой на обработку делать валидацию.... или это на блл сделать!!??!??!?!
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult CreateProfile(Profile newProfile)
        {
            if (newProfile.Id == Convert.ToInt32(HttpContext.Request.Cookies["Id"]))
            {
                var files = HttpContext.Request.Form.Files.FirstOrDefault();
                if(files != null)
                {
                    using(var stream = files.OpenReadStream())
                    {
                        _profile.Create(newProfile, stream);
                    }
                }
                //_profile.Create(newProfile);
                return RedirectToAction("Search", "Home");
            }
            throw new Exception("айди не совпадает с юзерским айди");
        }

        [HttpPost]
        public IActionResult UpdateUser(User newUser)
        {
            _user.Update(newUser);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            _user.Delete(id);
            return RedirectToAction("Index", "Home");
        }

        //-------------------------------------------------------------------------------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
