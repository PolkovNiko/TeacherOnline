using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL;
using TeacherOnline.DAL.Entities;
using TeacherOnline.DTO.ViewModel;
using TeacherOnline.Models;

namespace TeacherOnline.Controllers
{
    public class EstimateController : Controller
    {
        IProfile _profile;
        IGroup _group;
        ISubject _subject;
        IEstimate _estimate;
        IGroupsInSub _groupsInSub;

        public EstimateController(IGroup group, ISubject subject, IProfile profile, IEstimate estimate, IGroupsInSub groupsInSub)
        {
            _group = group;
            _subject = subject;
            _profile = profile;
            _estimate = estimate;
            _groupsInSub = groupsInSub;
        }

        //-------------------------------------------------------------------------------------------
        //Method Get

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new EstimateVM();
            vm.estimateList = _estimate.Find(u=>u.IdTeacher == (int)HttpContext.Session.GetInt32("Id"));
            return View(vm);
        }

        public IActionResult CreateEstimate()
        {
            var vm = new EstimateVM();
            //vm.subjects = _subject.Find(u => u.IdTeacher == (int)HttpContext.Session.GetInt32("Id"));
            vm.groups = _group.GetAll();
            //vm.users = _profile.Find(u => u.IdNavigation.Rank == "Study");
            return View(vm);
        }

        public IActionResult UpdateEstimate(int id)
        {
            var vm = new EstimateVM();
            vm.estimate = _estimate.Get(id);
            vm.subjects = _subject.Find(u => u.IdTeacher == (int)HttpContext.Session.GetInt32("Id"));
            vm.users = _profile.Find(u => u.IdNavigation.Rank == "Study");
            return View(_group.Get(id));
        }


        [HttpGet]
        public JsonResult GetUserBySubject(int subjectId, int groupId)
        {
            EstimateVM vm = new EstimateVM();
            var groups = _groupsInSub.Get(u => u.IdGroups == groupId && u.IdSubject == subjectId);
            var list = _profile.GetAll().Where(u => u.Groups == groups.IdGroups)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.LastName} {u.FirstName} {u.Otchestvo}"
                }).ToList();
            return Json(list);
        }
        
        [HttpGet]
        public JsonResult GetSubjectOnGroup(int groupId)
        {
            var list = _groupsInSub.GetAll().Where(u=> u.IdGroups == groupId && u.IdTeacher == (int)HttpContext.Session.GetInt32("Id"))
                .Select(u=> new SelectListItem
                {
                     Value = u.IdSubjectNavigation.Id.ToString(),
                     Text = u.IdSubjectNavigation.Name
                }).ToList();
            return Json(list);
        }

        //-------------------------------------------------------------------------------------------
        //Method Post


        [HttpPost]
        public IResult CreateEstimate(EstimateVM vm)
        {
            vm.estimate.IdTeacher = (int)HttpContext.Session.GetInt32("Id");
            _estimate.Create(vm.estimate);
            return Results.Redirect("Index");
        }

        [HttpPost]
        public IActionResult DeleteEstimate(int id)
        {
            _estimate.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateEstimate(EstimateVM vm)
        {
            vm.estimate.IdTeacher = (int)HttpContext.Session.GetInt32("Id");
            _estimate.Update(vm.estimate);
            return RedirectToAction("Index");
        }

        //-------------------------------------------------------------------------------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}