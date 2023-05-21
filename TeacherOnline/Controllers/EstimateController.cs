using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public EstimateController(IGroup group, ISubject subject, IProfile profile, IEstimate estimate)
        {
            _group = group;
            _subject = subject;
            _profile = profile;
            _estimate = estimate;
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
            vm.subjects = _subject.Find(u => u.IdTeacher == (int)HttpContext.Session.GetInt32("Id"));
            vm.users = _profile.Find(u => u.IdNavigation.Rank == "Study");
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