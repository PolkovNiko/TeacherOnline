using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL;
using TeacherOnline.Models;

namespace TeacherOnline.Controllers
{
    public class HomeController : Controller
    {
        IProfile _profile;
        
        public HomeController(IProfile profile)
        {
            _profile = profile;
        }

        //-------------------------------------------------------------------------------------------
        //Method Get


        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Teacher, Admin")]
        public IActionResult Search()
        {
            return View(_profile.GetAll());
        }





        //[HttpPost]
        //public IActionResult Search(Search querys)
        //{
        //    //querys.Fio = HttpContext.Request.Form["Fio"].ToString() == "on" ? true : false;
        //    //querys.Number = HttpContext.Request.Form["Number"].ToString() == "on" ? true : false;
        //    //querys.Adress = HttpContext.Request.Form["Adress"].ToString() == "on" ? true : false;
        //    //Dictionary<(bool, bool, bool), Func<IQueryable<Staff>>> quary = new Dictionary<(bool, bool, bool), Func<IQueryable<Staff>>>()
        //    //{
        //    //    [(true, false, false)] = () => { var x = db.Staff.Where(p => EF.Functions.Like(p.Fio, querys.Query + "%")); return x; },
        //    //    [(true, true, false)] = () => db.Staff.Where(p => EF.Functions.Like(p.Fio, querys.Query + "%") || EF.Functions.Like(p.Number, querys.Query + "%")),
        //    //    [(true, true, true)] = () => db.Staff.Where(p => EF.Functions.Like(p.Fio, querys.Query + "%") || EF.Functions.Like(p.Number, querys.Query + "%") || EF.Functions.Like(p.Adress, querys.Query + "%")),

        //    //    [(false, true, false)] = () => db.Staff.Where(p => EF.Functions.Like(p.Number, querys.Query + "%")),
        //    //    [(false, true, true)] = () => db.Staff.Where(p => EF.Functions.Like(p.Number, querys.Query + "%") || EF.Functions.Like(p.Adress, querys.Query + "%")),

        //    //    [(false, false, true)] = () => db.Staff.Where(p => EF.Functions.Like(p.Adress, querys.Query + "%")),
        //    //    [(true, false, true)] = () => db.Staff.Where(p => EF.Functions.Like(p.Adress, querys.Query + "%") || EF.Functions.Like(p.Fio, querys.Query + "%"))
        //    //};
        //    //var result = quary[(querys.Fio, querys.Number, querys.Adress)];
        //    var result = db.Profiles.Where(p => EF.Functions.Like(p.LastName, querys.Query) ||
        //        EF.Functions.Like(p.FirstName, querys.Query) ||
        //        EF.Functions.Like(p.Otchestvo, querys.Query));
        //    if (result != null)
        //    {
        //        return View(result.ToList());
        //    }
        //    return NotFound();
        //}

        //-------------------------------------------------------------------------------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}