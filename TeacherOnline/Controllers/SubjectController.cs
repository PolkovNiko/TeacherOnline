using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.BLL.Services;
using TeacherOnline.DAL;
using TeacherOnline.DAL.Entities;
using TeacherOnline.Models;

namespace TeacherOnline.Controllers
{
    public class SubjectController : Controller
    {
        ISubject _subject;
        IProfile _profile;
        IEstimate _estimate;


        public SubjectController(ISubject subject, IProfile profile, IEstimate estimate) 
        { 
            _subject = subject;
            _profile = profile;
            _estimate = estimate;
        }


        //-------------------------------------------------------------------------------------------
        //Method Get


        public IActionResult Subject()
        {
            ViewData["Id"] = HttpContext.Session.GetInt32("Id").ToString(); //пересмотреть отправляемые данные
            return View(_subject.GetAll());
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult CreateSub()
        {
            //ViewData["Id"] = HttpContext.Request.Cookies["Id"]; //пересмотреть отправляемые данные
            //тут через include нужно реализовать выборку учителей, учащихся и премета
            return View(_subject.GetAll());
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult StudyOfSub(int? id)
        {
            //ViewData["dep"] = id; //пересмотреть отправляемые данные
            return View(_profile.GetAll());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateSub(int? id)
        {
            return View(_subject.GetAll());
        }


        [Authorize(Roles = "Study")]
        public IActionResult Estimate()
        {
            // тут тоже пишится запрос с include учителей, учащихся и предмета
            return View(_estimate.GetAll());
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult CreateEst()
        {
            //аналогично прошлой ситуации
            return View(_estimate.GetAll());
        }


        //а чё это нужно? и в пост методах так же.. странно
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteEst(int id)
        {
            _estimate.Delete(id);
            return RedirectToAction("Subject");
        }

        //-------------------------------------------------------------------------------------------
        //Method Post


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IResult CreateSub(Subject sub)
        {
            _subject.Create(sub);
            return Results.Redirect("Subject");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteSub(int id)
        {
            _subject.Delete(id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UpdateSub(Subject dep)
        {
            _subject.Update(dep);
            return Ok();
        }

        //-------------------------------------------------------------------------------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
