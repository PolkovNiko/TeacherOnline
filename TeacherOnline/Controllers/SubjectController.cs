using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeacherOnline.BLL.Services;
using TeacherOnline.DAL;
using TeacherOnline.DAL.Entities;
using TeacherOnline.Models;

namespace TeacherOnline.Controllers
{
    public class SubjectController : Controller
    {
        SubjectService _subject;
        ProfileService _profile;
        EstimateService _estimate;


        public SubjectController(SubjectService subject, ProfileService profile, EstimateService estimate) 
        { 
            _subject = subject;
            _profile = profile;
            _estimate = estimate;
        }


        //-------------------------------------------------------------------------------------------
        //Method Get


        public IActionResult Subject()
        {
            ViewData["Id"] = HttpContext.Request.Cookies["Id"]; //пересмотреть отправляемые данные
            return View(_subject.GetAll());
        }

        [Authorize(Policy = "Admin")]
        public IActionResult CreateSub()
        {
            ViewData["Id"] = HttpContext.Request.Cookies["Id"]; //пересмотреть отправляемые данные
            //тут через include нужно реализовать выборку учителей, учащихся и премета
            return View(_subject.GetAll());
        }

        [Authorize(Policy = "Teacher")]
        public IActionResult StudyOfSub(int? id)
        {
            ViewData["dep"] = id; //пересмотреть отправляемые данные
            return View(_profile.GetAll());
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult UpdateSub(int? id)
        {
            return View(_subject.GetAll());
        }


        [Authorize(Policy = "Study")]
        public IActionResult Estimate()
        {
            // тут тоже пишится запрос с include учителей, учащихся и предмета
            return View(_estimate.GetAll());
        }

        [Authorize(Policy = "Teacher")]
        public IActionResult CreateEst()
        {
            //аналогично прошлой ситуации
            return View(_estimate.GetAll());
        }


        //а чё это нужно? и в пост методах так же.. странно
        [Authorize(Policy = "Admin")]
        public IActionResult DeleteEst(int id)
        {
            _estimate.Delete(id);
            return RedirectToAction("Subject");
        }

        //-------------------------------------------------------------------------------------------
        //Method Post


        [Authorize(Policy = "Admin")]
        [HttpPost]
        public IResult CreateSub(Subject sub)
        {
            _subject.Create(sub);
            return Results.Redirect("Subject");
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public IActionResult DeleteSub(int id)
        {
            _subject.Delete(id);
            return Ok();
        }

        [Authorize(Policy = "Admin")]
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
