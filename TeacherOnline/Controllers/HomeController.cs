using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.Models;

namespace TeacherOnline.Controllers
{
    public class HomeController : Controller
    {
        IProfile _profile;
        IUser _user;
        
        public HomeController(IProfile profile, IUser user)
        {
            _profile = profile;
            _user = user;
        }

        //-------------------------------------------------------------------------------------------
        //Method Get


        public IActionResult Index()
        {
            return View(_user.GetAll());
        }

        public IActionResult Search()
        {
            return View(_profile.GetAll());
        }

        //-------------------------------------------------------------------------------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}