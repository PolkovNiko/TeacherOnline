using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using TeacherOnline.BLL.Services;
using TeacherOnline.Models;
using TeacherOnline.DAL.Entities;
using TeacherOnline.BLL.Interfaces;
using System.Security.Claims;

namespace TeacherOnline.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IUser _user;
        IProfile _profile;
        IAuth _auth;

        public UsersController(ILogger<HomeController> logger, IUser user, IProfile profile, IAuth auth)
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
        [Authorize(Roles = "Admin")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Study, Teacher")] //тут же добавиться только авторизированным пользователям
        public  IActionResult UserProfile()
        {
            var tempuser = _profile.Get((int)HttpContext.Session.GetInt32("Id"));
            if (tempuser is null) return RedirectToAction("CreateProfile");
            return View(tempuser);
        }

        [HttpGet]
        [Authorize(Roles = "Study, Teacher")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUser(int id)
        {
            var tempuser = _user.Get(id);
            return View(tempuser);
        }

        //Method Post

        [HttpPost]
        public async Task<IResult> Autorization(User users)
        {
            await HttpContext.SignInAsync(_auth.LogIn(users));
            HttpContext.Session.SetInt32("Id", _auth.Id);
            return Results.Redirect("/");
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
            newProfile.Id = (int)HttpContext.Session.GetInt32("Id");
            if (HttpContext.Request.Form.Files.Count == 0)
                _profile.Update(newProfile, null);
            else 
            {
                var files = HttpContext.Request.Form.Files.FirstOrDefault();
                if (files != null)
                {
                    using (var stream = files.OpenReadStream())
                    {
                        _profile.Create(newProfile, stream);
                    }
                }
            } 
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
