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
    [Authorize(Roles = "Teacher")]
    public class GroupController : Controller
    {
        IGroup _group;
        IGroupsInSub _groupInSub;
        ISubject _subject;
        
        public GroupController(IGroup group, IGroupsInSub groupInSub, ISubject subject)
        {
            _group = group;
            _groupInSub = groupInSub;
            _subject = subject;
        }

        //-------------------------------------------------------------------------------------------
        //Method Get

        [HttpGet]
        public IActionResult Index()
        {
            GroupInSubVM vm = new GroupInSubVM();
            vm.groups = _group.GetAll();
            return View(vm);
        }

        public IActionResult CreateGroup()
        {
            return View();
        }

        public IActionResult UpdateGroup(int id)
        {
            return View(_group.Get(id));
        }

        [HttpGet]
        public IActionResult GroupInSub()
        {
            ViewData["Id"] = HttpContext.Session.GetInt32("Id");
            GroupInSubVM vm = new GroupInSubVM();
            vm.gisList = _groupInSub.GetAll();
            return View(vm);
        }

        public IActionResult CreateGroupInSub()
        {
            GroupInSubVM vm = new GroupInSubVM();
            vm.groups = _group.GetAll();
            vm.subjects = _subject.Find(u=> u.IdTeacher == (int)HttpContext.Session.GetInt32("Id"));
            return View(vm);
        }
        
        public IActionResult UpdateGroupInSub(int id)
        {
            GroupInSubVM vm = new GroupInSubVM();
            vm.groupsInSub = _groupInSub.Get(id);
            vm.groups = _group.GetAll();
            vm.subjects = _subject.Find(u => u.IdTeacher == (int)HttpContext.Session.GetInt32("Id"));
            return View(vm);
        }


        //-------------------------------------------------------------------------------------------
        //Method Post


        [HttpPost]
        public IResult CreateGroup(Group vm)
        {
            _group.Create(vm);
            return Results.Redirect("Index");
        }

        [HttpPost]
        public IActionResult DeleteGroup(int id)
        {
            _group.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateGroup(Group vm)
        {
            _group.Update(vm);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IResult CreateGroupInSub(GroupInSubVM vm)
        {
            vm.groupsInSub.IdTeacher = (int)HttpContext.Session.GetInt32("Id");
            _groupInSub.Create(vm.groupsInSub);
            return Results.Redirect("GroupInSub");
        }

        [HttpPost]
        public IActionResult DeleteGroupInSub(int id)
        {
            _groupInSub.Delete(id);
            return RedirectToAction("GroupInSub");
        }

        [HttpPost]
        public IActionResult UpdateGroupInSub(GroupInSubVM vm)
        {
            vm.groupsInSub.IdTeacher = (int)HttpContext.Session.GetInt32("Id");
            _groupInSub.Update(vm.groupsInSub);
            return RedirectToAction("GroupInSub");
        }


        //-------------------------------------------------------------------------------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}