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
            //var info = _db.Profiles.ToList();
            //tempSub sub = new tempSub();
            //List<tempSub.Teach> templist = new List<tempSub.Teach>();
            //foreach (var item in info)
            //{
            //    string fio = item.LastName + " " + item.FirstName + " " + item.Otchestvo;
            //    templist.Add(new tempSub.Teach(item.Id, fio));
            //}
            //if(templist != null)
            //    sub.Teachers = new SelectList(templist, "id", "fio");
            //return View(sub);
            //тут через include нужно реализовать выборку учителей, учащихся и премета
            return Ok();
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
            //if (id != null)
            //{
            //    Subject? deps = _db.Subjects.FirstOrDefault(p => p.Id == id);
            //    if (deps != null)
            //    {
            //        //ViewData["Id"] = HttpContext.Request.Cookies["Id"];
            //        ViewData["idsub"] = deps.Id;
            //        return View(_db.Subjects.ToList());
            //    }
            //}
            //return Forbid();
            return View(_subject.GetAll());
        }


        [Authorize(Policy = "Study")]
        public IActionResult Estimate()
        {
            //var info = _db.Profiles.ToList();
            //Estimate sub = new Estimate();
            //List<Estimate.Teach> teachlist = new List<Estimate.Teach>();
            //foreach (var item in info)
            //{
            //    string fio = item.LastName + " " + item.FirstName + " " + item.Otchestvo;
            //    teachlist.Add(new Estimate.Teach(item.Id, fio));
            //}
            //if (teachlist != null)
            //    sub.teach = new SelectList(teachlist, "id", "fio");

            //List<Estimate.User> userlist = new List<Estimate.User>();
            //foreach (var item in info)
            //{
            //    string fio = item.LastName + " " + item.FirstName + " " + item.Otchestvo;
            //    userlist.Add(new Estimate.User(item.Id, fio));
            //}
            //if (teachlist != null)
            //    sub.user = new SelectList(teachlist, "id", "fio");

            //List<Estimate.Subj> templist = new List<Estimate.Subj>();
            //foreach (var item in info)
            //{
            //    string fio = item.LastName + " " + item.FirstName + " " + item.Otchestvo;
            //    templist.Add(new Estimate.Subj(item.Id, fio));
            //}
            //if (templist != null)
            //    sub.subj = new SelectList(templist, "id", "name");

            //return View(sub);


            // тут тоже пишится запрос с include учителей, учащихся и предмета
            return Ok();
        }

        [Authorize(Policy = "Teacher")]
        public IActionResult CreateEst()
        {
            //var info = _db.Profiles.ToList();
            //Estimate sub = new Estimate();
            //List<Estimate.Teach> teachlist = new List<Estimate.Teach>();
            //foreach (var item in info)
            //{
            //    string fio = item.LastName + " " + item.FirstName + " " + item.Otchestvo;
            //    teachlist.Add(new Estimate.Teach(item.Id, fio));
            //}
            //if (teachlist != null)
            //    sub.teach = new SelectList(teachlist, "id", "fio");

            //List<Estimate.User> userlist = new List<Estimate.User>();
            //foreach (var item in info)
            //{
            //    string fio = item.LastName + " " + item.FirstName + " " + item.Otchestvo;
            //    userlist.Add(new Estimate.User(item.Id, fio));
            //}
            //if (teachlist != null)
            //    sub.user = new SelectList(teachlist, "id", "fio");

            //List<Estimate.Subj> templist = new List<Estimate.Subj>();
            //foreach (var item in info)
            //{
            //    string fio = item.LastName + " " + item.FirstName + " " + item.Otchestvo;
            //    templist.Add(new Estimate.Subj(item.Id, fio));
            //}
            //if (templist != null)
            //    sub.subj = new SelectList(templist, "id", "name");


            //return View(sub);


            //аналогично прошлой ситуации
            return Ok();
        }


        //а чё это нужно? и в пост методах так же.. странно
        [Authorize(Policy = "Admin")]
        public IActionResult DeleteEst(int id)
        {
            //if (id != null)
            //{
            //    Estimate? dep = _db.Estimates.FirstOrDefault(p => p.Id == id); 
            //    if (dep != null)
            //    {
            //        _db.Estimates.Remove(dep);
            //        _db.SaveChanges();
            //        return RedirectToAction("Subject");
            //    }
            //}
            //return Forbid();
            _estimate.Delete(id);
            return RedirectToAction("Subject");
        }

        //-------------------------------------------------------------------------------------------
        //Method Post


        [Authorize(Policy = "Admin")]
        [HttpPost]
        public IResult CreateSub(Subject sub)
        {
            //if(sub is null) { return Results.BadRequest("Данные не введены"); }
            //var id = _db.Subjects.Max(p => p.Id);
            //id++;
            //Subject subs = new Subject()
            //{
            //    Id = id,
            //    Name = sub.Name,
            //    IdTeacher = sub.IdTeacher,
            //    About = sub.About
            //};
            //_db.Subjects.Add(subs);
            //_db.SaveChanges();
            _subject.Create(sub);
            return Results.Redirect("Subject");
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public IActionResult DeleteSub(int id)
        {
            //if (id != null)
            //{
            //    Subject? dep = _db.Subjects.FirstOrDefault(p => p.Id == id); //&& p.IdTeacher.ToString() == HttpContext.Request.Cookies["Id"]);
            //    if (dep != null)
            //    {
            //        _db.Subjects.Remove(dep);
            //        _db.SaveChanges();
            //        return RedirectToAction("Subject");
            //    }
            //}
            //return Forbid();
            _subject.Delete(id);
            return Ok();
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateSub(Subject dep)
        {
            //if (dep != null)
            //{
            //    _db.Subjects.Update(dep);
            //    await _db.SaveChangesAsync();
            //    return RedirectToAction("Subject");
            //    //return View("Departments", _db.Departmens.ToList());
            //}
            //return NotFound();
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
