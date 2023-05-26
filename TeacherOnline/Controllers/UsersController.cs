using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using TeacherOnline.Models;
using TeacherOnline.DAL.Entities;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DTO.ModelsDTO;
using TeacherOnline.DTO.ViewModel;
using TeacherOnline.DTO;

namespace TeacherOnline.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IUser _user;
        IProfile _profile;
        IAuth _auth;
        IGroup _group;
        IConvertModels _convert;

        public UsersController(ILogger<HomeController> logger, IUser user, IProfile profile, IAuth auth, IGroup group, IConvertModels convert)
        {
            _logger = logger;
            _user = user;
            _profile = profile;
            _auth = auth;
            _group = group;
            _convert = convert;
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
            UserProfileVM vm = new UserProfileVM();
            vm.Group = _group.GetAll();
            vm.Profile = _profile.Get((int)HttpContext.Session.GetInt32("Id"));
            if (vm.Profile is null) return RedirectToAction("CreateProfile");
            return View(vm);
        }

        [HttpGet]
        public IActionResult CheckProfile(int id)
        {
            //if(id == (int)HttpContext.Session.GetInt32("Id"))
            //{
                UserProfileVM vm = new UserProfileVM();
                vm.Profile = _profile.Get(id);
                return View(vm);
            //}
            //else
            //{
            //    return RedirectToAction("UserProfile");
            //}
        }

        [HttpGet]
        [Authorize(Roles = "Study, Teacher")]
        public IActionResult CreateProfile()
        {
            UserProfileVM vm = new UserProfileVM();
            vm.Group = _group.GetAll();
            return View(vm);
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
            //var index = _user.GetAll();
            //user.Id = index.Count() == 0 ? 1 : index.Count() + 1;

            _user.Create(user);
            return RedirectToAction("Index", "Home");
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

        [HttpPost]
        public IActionResult CreateProfile(UserProfileVM newProfile)
        {
            newProfile.Profile.Id = (int)HttpContext.Session.GetInt32("Id");
            var files = HttpContext.Request.Form.Files.FirstOrDefault();
            if(files != null)
            {
                using(var stream = files.OpenReadStream())
                {
                    _profile.Create(_convert.ConvetToProfile(newProfile), stream);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult UpdateProfile(UserProfileVM newProfile)
        {
            newProfile.Profile.Id = (int)HttpContext.Session.GetInt32("Id");
            if (HttpContext.Request.Form.Files.Count == 0)
                _profile.Update(newProfile.Profile, null);
            else 
            {
                var files = HttpContext.Request.Form.Files.FirstOrDefault();
                if (files != null)
                {
                    using (var stream = files.OpenReadStream())
                    {
                        _profile.Create(newProfile.Profile, stream);
                    }
                }
            } 
            //естественно перепревкой на обработку делать валидацию.... или это на блл сделать!!??!??!?!
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
