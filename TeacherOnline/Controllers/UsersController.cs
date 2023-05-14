using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
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
            //var data = db.Profiles.FirstOrDefault(test => test.Id.ToString() == HttpContext.Request.Cookies["Id"]);
            //if (data is null) 
            //{
            //    //newProfile st = new newProfile() { Id = Convert.ToInt32(HttpContext.Request.Cookies["Id"]) };
            //    //ViewData["Id"] = HttpContext.Request.Cookies["Id"];
            //    return RedirectToAction("CreateProfile");
            //} 
            //return View(data); //тут выборка на данные авторизированного пользвателя(теперь правильно)
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
            //if (id != null)
            //{
            //    User? users = db.Users.FirstOrDefault(p => p.Id == id);
            //    if (users != null)
            //    {
            //        ViewData["idUser"] = users.Id;
            //        return View(db.Users.ToList());
            //    }
            //}
            //return NotFound();
        }



        //Method Post

        [HttpPost]
        public async Task<IResult> Autorization(User users, string? returnUrl)
        {
            await HttpContext.SignInAsync(_auth.LogIn(users));
            return Results.Redirect(returnUrl);
            //if(users.Login.IsNullOrEmpty() || users.Password.IsNullOrEmpty()) return Results.BadRequest("Поля не заполнены");
            //User? valid = db.Users.FirstOrDefault(val => val.Login == users.Login && val.Password == users.Password);
            //if (valid is null) return Results.Unauthorized();
            //var claims = new List<Claim> { new Claim(ClaimTypes.Role, valid.Rank) };
            //var claimIdentitys = new ClaimsIdentity(claims, "Cookies");
            //var claimParticle = new ClaimsPrincipal(claimIdentitys);
            //await HttpContext.SignInAsync(claimParticle);
            //HttpContext.Response.Cookies.Append("Id", valid.Id.ToString());
            //HttpContext.Response.Cookies.Append("rank", valid.Rank);
            //ViewData["Id"] = valid.Id.ToString();
            //return Results.Redirect(returnUrl??"/");
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            //db.Users.Add(user);
            //await db.SaveChangesAsync();
            _user.Create(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult UpdateProfile(Profile newProfile)
        {
            //if (newProfile == null)
            //{
            //    return BadRequest("Поля не заполнены!");
            //}
            //Profile? temp = db.Profiles.FirstOrDefault(u=>u.Id == newProfile.Id);
            //string log = "";
            //if (temp != null)
            //{
            //    //log += "//------------------------------------------------------------------------------------//";
            //    //log += temp.Fio != newProfile.Fio ? newProfile.Fio + "\n" : "";
            //    //log += temp.Birthday != newProfile.Birthday ? newProfile.Birthday + "\n" : "";
            //    //log += temp.Number != newProfile.Number ? newProfile.Number + "\n" : "";
            //    //log += temp.Adress != newProfile.Adress ? newProfile.Adress + "\n" : "";
            //    //log += temp.FamilyStatus != newProfile.FamilyStatus ? newProfile.FamilyStatus + "\n" : "";
            //    //log += temp.Wage != newProfile.Wage ? newProfile.Wage + "\n" : "";
            //    //log += temp.Department != newProfile.Department ? newProfile.Department + "\n" : "";

            //    temp.FirstName = newProfile.FirstName;
            //    temp.LastName = newProfile.LastName;
            //    temp.Otchestvo = newProfile.Otchestvo;
            //    temp.Date = newProfile.Date;
            //    temp.Group = newProfile.Group;
            //    temp.About = newProfile.About;
            //}
            //else Results.NotFound();
            //byte[] image = null;
            //if(newProfile.Photo != null)
            //{
            //    using (var binaryReader = new BinaryReader(newProfile.Photos.OpenReadStream()))
            //    {
            //        image = binaryReader.ReadBytes((int)newProfile.Photos.Length);
            //    }
            //    //log += temp.Photo.ToString() != newProfile.Photo.ToString() ? "Изменено фото \n" : "";
            //    log += "//------------------------------------------------------------------------------------//";
            //    temp.Photo = image;
            //}
            //db.Profiles.Update(temp);
            //await db.SaveChangesAsync();
            //_logger.LogInformation(log + $"{DateTime.Now} \n");
            _profile.Update(newProfile);
            //естественно перепревкой на обработку делать валидацию.... или это на блл сделать!!??!??!?!
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile(Profile newProfile)
        {
            //тут подумать над реализацией добавления фото(у чела одолжить алгоритм преобразования без промежуточной формы через извлечение из httpcontext.response.form.file
            //var i = db.Users.FirstOrDefault(u => u.Id == newProfile.Id);
            //if(i != null)
            //{
            //    Profile staff = new Profile
            //    {
            //        Id = i.Id,
            //        LastName = newProfile.LastName,
            //        FirstName = newProfile.FirstName,
            //        Otchestvo = newProfile.Otchestvo,
            //        Date = newProfile.Date,
            //        Group = newProfile.Group,
            //        About = newProfile.About
            //    };
            //    using (var binaryReader = new BinaryReader(HttpContext.Request.Form.Files[0].OpenReadStream()))
            //    {
            //        byte[] image = null;
            //        image = binaryReader.ReadBytes((int)HttpContext.Request.Form.Files[0].Length);
            //        staff.Photo = image;
            //    }
            //    db.Profiles.Add(staff);
            //    await db.SaveChangesAsync();

            //}
            if (newProfile.Id == Convert.ToInt32(HttpContext.Request.Cookies["Id"]))
            {
                _profile.Create(newProfile);
                return RedirectToAction("Search", "Home");
            }
            throw new Exception("айди не совпадает с юзерским айди");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User newUser)
        {
            _user.Update(newUser);
            return RedirectToAction("Index", "Home");
            //if(newUser != null)
            //{
            //    User? newU = db.Users.FirstOrDefault(p=> p.Id == newUser.Id);
            //    if(newU != null)
            //    {
            //        newU.Login = newUser.Login;
            //        newU.Password = newUser.Password;
            //        newU.Email = newUser.Email;
            //        newU.Rank = newUser.Rank;
            //        db.Users.Update(newU);
            //        await db.SaveChangesAsync();
            //        return RedirectToAction("Index", "Home");
            //    }

            //}
            //return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
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
