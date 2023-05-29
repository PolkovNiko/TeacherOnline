﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
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
            ViewData["Id"] = (int)HttpContext.Session.GetInt32("Id");
            var vm = new EstimateVM();
            if(User.IsInRole("Teacher"))
                vm.estimateList = _estimate.Find(u=>u.IdTeacher == (int)HttpContext.Session.GetInt32("Id"));
            else
            {
                vm.estimateList = _estimate.Find(u => u.IdUser == (int)HttpContext.Session.GetInt32("Id"));
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles ="Teacher")]
        public IActionResult CreateEstimate()
        {
            var vm = new EstimateVM();
            vm.groups = _group.GetAll();
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult UpdateEstimate(int id)
        {
            var vm = new EstimateVM();
            vm.estimate = _estimate.Get(id);
            //алгоритм изьятия студента, зная номер предмета
            vm.OneGroup = _groupsInSub.Get(u => u.IdSubject == vm.estimate.IdSubject).IdGroupsNavigation;
            vm.users = _profile.Find(u => u.IdNavigation.Rank == "Study" && u.Groups == vm.OneGroup.Id);

            vm.groups = _group.GetAll();
            vm.subjects = _groupsInSub.Find(u =>u.IdGroups == vm.OneGroup.Id && u.IdSubjectNavigation.IdTeacher == (int)HttpContext.Session.GetInt32("Id")).Select(u=>u.IdSubjectNavigation);
            return View(vm);
        }

        [HttpGet]
        public IActionResult AverageEstimate(int id)
        {
            var vm = new EstimateVM();
            vm.OneGroup = _groupsInSub.Get(u => u.IdSubject == id).IdGroupsNavigation;
            vm.users = _profile.Find(u => u.IdNavigation.Rank == "Study" && u.Groups == vm.OneGroup.Id);
            vm.subject = _subject.Get(id);
            vm.estimateList = _estimate.Find(u => u.IdTeacher == (int)HttpContext.Session.GetInt32("Id") || u.IdSubject == id).OrderBy(u => u.IdUser);
            List<Estimate> tempList = vm.estimateList.OrderBy(u => u.IdUser).ToList();
            int currentUser = 0;
            var estList = AvgEst(vm, tempList, currentUser, id);
            return View(vm);
        }
        [HttpGet]
        public IActionResult GetStats(int id)
        {
            var vm = new EstimateVM();
            //vm.estimateList = _estimate.Find(u=> u.IdSubject == 3);
            vm.user = _profile.Get(id);
            vm.subjects = _groupsInSub.Find(u => u.IdGroups == vm.user.Groups).Select(u => u.IdSubjectNavigation);
            //vm.subject = _subject.Get(3);
            //vm.CountEst = CountEstimates(vm.estimateList);
            return View(vm);
        }

        [HttpGet]
        public IActionResult GetStat(EstimateVM vm)
        {
            vm.estimateList = _estimate.Find(u => u.IdSubject == vm.subject.Id && u.IdUser == vm.user.Id);
            vm.user = _profile.Get(vm.user.Id);
            vm.subjects = _groupsInSub.Find(u=> u.IdGroups == vm.user.Groups).Select(u=>u.IdSubjectNavigation);
            vm.subject = _subject.Get(vm.subject.Id);
            vm.CountEstTeory = CountEstimateTeoty(vm.estimateList);
            vm.CountEstPrakt = CountEstimatePrakt(vm.estimateList);
            vm.CountEstOKR = CountEstimateOKR(vm.estimateList);
            vm.AvgEstimate = AvgEstToUser(vm.estimateList.ToList(), vm.user.Id, vm.subject.Id, vm.AvgEstimate);
            return View("GetStats", vm);
        }
        private Dictionary<int,int> CountEstimateTeoty(IEnumerable<Estimate> list)
        {
            EstimateVM vm = new EstimateVM();
            foreach (var item in list)
            {
                if(item.Type == 0)
                {
                    if (vm.CountEstTeory.IsNullOrEmpty())
                    {
                        vm.CountEstTeory.Add(item.Score, 1);
                        continue;
                    }
                    if (vm.CountEstTeory.ContainsKey(item.Score))
                    {
                        vm.CountEstTeory[item.Score]++;
                        continue;
                    }
                    else
                    {
                        vm.CountEstTeory.Add(item.Score, 1);
                        continue;
                    }
                }
                continue;
            }
            return vm.CountEstTeory;
        }
        private Dictionary<int, int> CountEstimatePrakt(IEnumerable<Estimate> list)
        {
            EstimateVM vm = new EstimateVM();
            foreach (var item in list)
            {
                if (item.Type == 1)
                {
                    if (vm.CountEstPrakt.IsNullOrEmpty())
                    {
                        vm.CountEstPrakt.Add(item.Score, 1);
                        continue;
                    }
                    if (vm.CountEstPrakt.ContainsKey(item.Score))
                    {
                        vm.CountEstPrakt[item.Score]++;
                        continue;
                    }
                    else
                    {
                        vm.CountEstPrakt.Add(item.Score, 1);
                        continue;
                    }
                }
                continue;
            }
            return vm.CountEstPrakt;
        }
        private Dictionary<int, int> CountEstimateOKR(IEnumerable<Estimate> list)
        {
            EstimateVM vm = new EstimateVM();
            foreach (var item in list)
            {
                if (item.Type == 2)
                {
                    if (vm.CountEstOKR.IsNullOrEmpty())
                    {
                        vm.CountEstOKR.Add(item.Score, 1);
                        continue;
                    }
                    if (vm.CountEstOKR.ContainsKey(item.Score))
                    {
                        vm.CountEstOKR[item.Score]++;
                        continue;
                    }
                    else
                    {
                        vm.CountEstOKR.Add(item.Score, 1);
                        continue;
                    }
                }
                continue;
            }
            return vm.CountEstOKR;
        }
        public Dictionary<int, double> AvgEstToUser(List<Estimate> tempList, int currentUser, int id, Dictionary<int, double> avg)
        {
            double tempAvg = 0.0;
            double est = 0.0;
            double okr = 0.0;
            int iEst = 0;
            int iOkr = 0;
            foreach (var items in tempList)
            {
                if (currentUser == items.IdUser && items.IdSubject == id)
                {
                    if (items.Type != 2)
                    {
                        est += items.Score;
                        iEst++;
                        continue;
                    }
                    else
                    {
                        okr += items.Score;
                        iOkr++;
                        continue;
                    }
                }
                continue;
            }
            if (iEst == 0)
            {
                tempAvg = okr / iOkr;
            }
            else
            {
                if (iOkr == 0)
                {
                    tempAvg = est / iEst;
                }
                else
                {
                    tempAvg = (est / iEst + okr / iOkr) / 2;
                }
            }
            avg.Add(currentUser, tempAvg);
            return avg;
        }
        public EstimateVM AvgEst(EstimateVM vm, List<Estimate> tempList, int currentUser, int id)
        {
            foreach (var item in vm.users)
            {
                currentUser = item.Id;
                float tempAvg = 0;
                float est = 0;
                float okr = 0;
                //int user = 0;
                int iEst = 0;
                int iOkr = 0;
                foreach (var items in tempList)
                {
                    if (currentUser == items.IdUser && items.IdSubject == id)
                    {
                        if (items.Type != 2)
                        {
                            est += items.Score;
                            iEst++;
                            continue;
                        }
                        else
                        {
                            okr += items.Score;
                            iOkr++; 
                            continue;
                        }
                    }
                    continue;
                }
                if(iEst == 0)
                {
                    tempAvg = okr / iOkr;
                }
                else
                {
                    if(iOkr == 0)
                    {
                        tempAvg = est / iEst;
                    }
                    else
                    {
                        tempAvg = (est / iEst + okr / iOkr) / 2;
                    }
                }
                vm.AvgEstimate.Add(currentUser, tempAvg);
                foreach (var items in tempList.ToList())
                {
                    if (currentUser == items.IdUser)
                    {
                        tempList.Remove(items);
                        continue;
                    }
                }
            }

            return vm;
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
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
        [Authorize(Roles = "Teacher")]
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
        [Authorize(Roles = "Teacher")]
        public IResult CreateEstimate(EstimateVM vm)
        {
            vm.estimate.IdTeacher = (int)HttpContext.Session.GetInt32("Id");
            vm.estimate.DateAdd = DateTime.Now;
            vm.estimate.DateUpdate = DateTime.Now;
            _estimate.Create(vm.estimate);
            return Results.Redirect("Index");
        }


        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult UpdateEstimate(EstimateVM vm)
        {
            vm.estimate.IdTeacher = (int)HttpContext.Session.GetInt32("Id");
            vm.estimate.DateUpdate = DateTime.Now;
            _estimate.Update(vm.estimate);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult DeleteEstimate(int id)
        {
            _estimate.Delete(id);
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