using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Linq;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL.Entities;
using TeacherOnline.DTO.ViewModel;
using TeacherOnline.Models;
using static TeacherOnline.DTO.ViewModel.UserProfileVM;

namespace TeacherOnline.Controllers
{
    public class HomeController : Controller
    {
        IProfile _profile;
        IUser _user;
        


        public HomeController(IProfile profile, IUser user)
        {
            _profile = profile;
            _user = user;
        }

        //-------------------------------------------------------------------------------------------
        //Method Get


        public IActionResult Index()
        {
            return View(_user.GetAll());
        }

        public IActionResult UserList(string roles, string lastn, string firstn, string otch ,SortStateProfile sortOrder = SortStateProfile.LastNameAsc)
        {
            UserProfileVM vm = new UserProfileVM();
            vm.profileList = _profile.GetAll();
            if (roles == "0") roles = null;
            if (roles.IsNullOrEmpty())
            {
                if (lastn.IsNullOrEmpty() && firstn.IsNullOrEmpty() && otch.IsNullOrEmpty())
                    vm.profileList = _profile.GetAll();
                else if (lastn.IsNullOrEmpty() && firstn.IsNullOrEmpty() && !otch.IsNullOrEmpty())
                {
                    vm.profileList = _profile.Find(u => u.Otchestvo.Contains(otch));
                }
                else if (lastn.IsNullOrEmpty() && !firstn.IsNullOrEmpty() && otch.IsNullOrEmpty())
                {
                    vm.profileList = _profile.Find(u => u.FirstName.Contains(firstn));
                }
                else if (lastn.IsNullOrEmpty() && !firstn.IsNullOrEmpty() && !otch.IsNullOrEmpty())
                {
                    vm.profileList = _profile.Find(u => u.FirstName.Contains(firstn) && u.Otchestvo.Contains(otch));
                }
                else if (!lastn.IsNullOrEmpty() && firstn.IsNullOrEmpty() && otch.IsNullOrEmpty())
                {
                    vm.profileList = _profile.Find(u => u.LastName.Contains(lastn));
                }
                else if (!lastn.IsNullOrEmpty() && firstn.IsNullOrEmpty() && !otch.IsNullOrEmpty())
                {
                    vm.profileList = _profile.Find(u => u.LastName.Contains(lastn) && u.Otchestvo.Contains(otch));
                }
                else if (!lastn.IsNullOrEmpty() && !firstn.IsNullOrEmpty() && otch.IsNullOrEmpty())
                {
                    vm.profileList = _profile.Find(u => u.LastName.Contains(lastn) && u.FirstName.Contains(firstn));
                }
                else //if (!lastn.IsNullOrEmpty() && !firstn.IsNullOrEmpty() && !otch.IsNullOrEmpty())
                {
                    vm.profileList = _profile.Find(u => u.LastName.Contains(lastn) && u.FirstName.Contains(firstn) && u.Otchestvo.Contains(otch));
                }
            }
            else
            {
                if (lastn.IsNullOrEmpty() && firstn.IsNullOrEmpty() && otch.IsNullOrEmpty())
                    vm.profileList = _profile.Find(u => u.IdNavigation.Rank == roles);
                else if (lastn.IsNullOrEmpty() && firstn.IsNullOrEmpty() && !otch.IsNullOrEmpty())
                {
                    vm.profileList = _profile.Find(u => u.IdNavigation.Rank == roles && u.Otchestvo.Contains(otch));
                }
                else if (lastn.IsNullOrEmpty() && !firstn.IsNullOrEmpty() && otch.IsNullOrEmpty())
                {
                    vm.profileList = _profile.Find(u => u.IdNavigation.Rank == roles && u.FirstName.Contains(firstn));
                }
                else if (lastn.IsNullOrEmpty() && !firstn.IsNullOrEmpty() && !otch.IsNullOrEmpty())
                {
                    vm.profileList = _profile.Find(u => u.IdNavigation.Rank == roles
                    && u.FirstName.Contains(firstn) && u.Otchestvo.Contains(otch));
                }
                else if (!lastn.IsNullOrEmpty() && firstn.IsNullOrEmpty() && otch.IsNullOrEmpty())
                {
                    vm.profileList = _profile.Find(u => u.IdNavigation.Rank == roles && u.LastName.Contains(lastn));
                }
                else if (!lastn.IsNullOrEmpty() && firstn.IsNullOrEmpty() && !otch.IsNullOrEmpty())
                {
                    vm.profileList = _profile.Find(u => u.IdNavigation.Rank == roles && u.LastName.Contains(lastn) && u.Otchestvo.Contains(otch));
                }
                else if (!lastn.IsNullOrEmpty() && !firstn.IsNullOrEmpty() && otch.IsNullOrEmpty())
                {
                    vm.profileList = _profile.Find(u => u.IdNavigation.Rank == roles && u.LastName.Contains(lastn) && u.FirstName.Contains(otch));
                }
                else if (!lastn.IsNullOrEmpty() && !firstn.IsNullOrEmpty() && !otch.IsNullOrEmpty())
                {
                    vm.profileList = _profile.Find(u => u.IdNavigation.Rank == roles
                    && u.LastName.Contains(lastn) && u.FirstName.Contains(firstn) && u.Otchestvo.Contains(otch));
                }
            }

            vm.profileList = sortOrder switch
            {
                SortStateProfile.LastNameDesc => vm.profileList.OrderByDescending(u => u.LastName),
                SortStateProfile.FirstNameAsc => vm.profileList.OrderBy(u => u.FirstName),
                SortStateProfile.FirstNameDesc => vm.profileList.OrderByDescending(u => u.FirstName),
                SortStateProfile.OtchestvoAsc => vm.profileList.OrderBy(u => u.Otchestvo),
                SortStateProfile.OtchestvoDesc => vm.profileList.OrderByDescending(u => u.Otchestvo),
                SortStateProfile.GroupsAsc => vm.profileList.OrderBy(u => u.GroupsNavigation?.Name),
                SortStateProfile.GroupsDesc => vm.profileList.OrderByDescending(u => u.GroupsNavigation?.Name),
                _ => vm.profileList.OrderBy(u => u.LastName)
            };
            vm.profileList.AsQueryable().AsNoTracking();
            vm.Str = new Sorted(sortOrder);
            vm.Fltr = new Filters(lastn, firstn, otch, roles);
            return View(vm);
        }

        public IActionResult Help()
        {
            return View();
        }

        //-------------------------------------------------------------------------------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}