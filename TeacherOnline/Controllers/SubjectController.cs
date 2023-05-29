using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        IGroupsInSub _groupsInSub;


        public SubjectController(ISubject subject, IProfile profile, IEstimate estimate, IUser user, IGroupsInSub groupsInSub) 
        { 
            _subject = subject;
            _profile = profile;
            _estimate = estimate;
            _user = user;
            _groupsInSub = groupsInSub;
        }


        //-------------------------------------------------------------------------------------------
        //Method Get


        public IActionResult Subject()
        {
            ViewData["Id"] = HttpContext.Session.GetInt32("Id").ToString(); //пересмотреть отправляемые данные
            if (User.IsInRole("Teacher"))
            {
                return View(_subject.Find(u => u.IdTeacher == (int)HttpContext.Session.GetInt32("Id")));
            }
            var user = _profile.Get((int)HttpContext.Session.GetInt32("Id"));
            if(user is null)
            {
                return RedirectToAction("UserProfile", "Users");
            }
            var list = _groupsInSub.Find(u => u.IdGroups == (int)user.Groups).Select(u=> u.IdSubjectNavigation);
            return View(list);
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult CreateSub()
        {
            SubjectGroupTeacherVM vm = new SubjectGroupTeacherVM();
            vm.teacher = _profile.Find(x => x.IdNavigation.Rank == "Teacher" && x.Id != HttpContext.Session.GetInt32("Id"));
            return View(vm);
        }

        public IActionResult StudyOfSub(int id)
        {
            ViewData["Idsub"] = id;
            var userList = _subject.GetStudyOfSub(id);
            return View(userList);
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
