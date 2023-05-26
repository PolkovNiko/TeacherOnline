using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.BLL.Services;
using TeacherOnline.DAL;
using TeacherOnline.DAL.Entities;
using TeacherOnline.DTO.ViewModel;
using TeacherOnline.Models;

namespace TeacherOnline.Controllers
{
    public class SubjectController : Controller
    {
        ISubject _subject;
        IProfile _profile;
        IEstimate _estimate;
        IUser _user;


        public SubjectController(ISubject subject, IProfile profile, IEstimate estimate, IUser user) 
        { 
            _subject = subject;
            _profile = profile;
            _estimate = estimate;
            _user = user;
        }


        //-------------------------------------------------------------------------------------------
        //Method Get


        public IActionResult Subject()
        {
            ViewData["Id"] = HttpContext.Session.GetInt32("Id").ToString(); //пересмотреть отправляемые данные
            return View(_subject.Find(u => u.IdTeacher == (int)HttpContext.Session.GetInt32("Id")));
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult CreateSub()
        {
            SubjectGroupTeacherVM vm = new SubjectGroupTeacherVM();
            vm.teacher = _profile.Find(x => x.IdNavigation.Rank == "Teacher" && x.Id != HttpContext.Session.GetInt32("Id"));
            return View(vm);
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult StudyOfSub(int id)
        {
            //ViewData["dep"] = id; //пересмотреть отправляемые данные
            var userList = _profile.GetAll();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult UpdateSub(int id)
        {
            SubjectGroupTeacherVM vm = new SubjectGroupTeacherVM();
            vm.teacher = _profile.Find(x => x.IdNavigation.Rank == "Teacher" && x.Id != HttpContext.Session.GetInt32("Id"));
            vm.sub = _subject.Get(id);
            return View(vm);
        }


        [Authorize(Roles = "Study, Teacher")]
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
        [Authorize(Roles = "Teacher")]
        public IActionResult DeleteEst(int id)
        {
            _estimate.Delete(id);
            return RedirectToAction("Subject");
        }

        public IActionResult GroupInSub()
        {
            return RedirectToAction("GroupInSub", "Group");
        }
        //-------------------------------------------------------------------------------------------
        //Method Post


        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public IResult CreateSub(Subject sub)
        {
            sub.IdTeacher = (int)HttpContext.Session.GetInt32("Id");
            _subject.Create(sub);
            return Results.Redirect("Subject");
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public IActionResult DeleteSub(int id)
        {
            _subject.Delete(id);
            return RedirectToAction("Subject");
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public IActionResult UpdateSub(SubjectGroupTeacherVM dep)
        {
            _subject.Update(dep.sub);
            return RedirectToAction("Subject");
        }

        //-------------------------------------------------------------------------------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
