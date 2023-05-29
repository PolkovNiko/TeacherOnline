using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL.Entities;
using TeacherOnline.DTO.ViewModel;
using File = TeacherOnline.DAL.Entities.File;

namespace TeacherOnline.Controllers
{
    public class FileController : Controller
    {
        IFile _file;
        IGroupsInSub _groupsInSub;
        IProfile _user;
        ISubject _sub;

        public FileController(IFile file, IGroupsInSub groupsInSub, IProfile profile, ISubject subject)
        {
            _file = file;
            _groupsInSub = groupsInSub;
            _user = profile;
            _sub = subject;
        }

        [HttpGet]
        public ActionResult Index(int id)
        {
            ViewData["Id"] = HttpContext.Session.GetInt32("Id").ToString();
            if(id == (int)HttpContext.Session.GetInt32("Id"))
            {
                FileVM vm = new FileVM();
                vm.user = _user.Get(id);
                vm.files = _file.GetAll().Where(u => u.IdUser == (int)HttpContext.Session.GetInt32("Id")).ToList();
                vm.subjects = _sub.GetAll().ToList();
                return View(vm);
            }
            else
            {
                FileVM vm = new FileVM();
                vm.user = _user.Get(id);
                vm.files = _file.GetAll().Where(u => u.IdUser == id).ToList();
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult UpdateFile(int id)
        {
            FileVM vm = new FileVM();
            vm.file = _file.Get(id);
            return View(vm);
        }

        [HttpGet]
        public ActionResult FileSub(int id)
        {
            FileVM vm = new FileVM();
            vm.files = _file.GetAll().Where(u => u.IdSub == id).ToList();
            vm.subject = _sub.Get(id);
            return View(vm);
        }

        [HttpGet]
        public JsonResult GetSubject()
        {
            var user = _user.Get((int)HttpContext.Session.GetInt32("Id"));
            if(user.Groups != null)
            {
                var list = _groupsInSub.Find(u => u.IdGroups == (int)user.Groups).Select(u => new SelectListItem
                {
                    Value = u.IdSubjectNavigation.Id.ToString(),
                    Text = u.IdSubjectNavigation.Name
                });
                return Json(list);
            }
            else
            {
                var list = _groupsInSub.Find(u => u.IdTeacher == user.Id).Select(u => new SelectListItem
                {
                    Value = u.IdSubjectNavigation.Id.ToString(),
                    Text = u.IdSubjectNavigation.Name
                });
                return Json(list);
            }
        }

        [HttpPost]
        public IActionResult CreateFile(FileVM vm)
        {
            var item = vm.file;
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            if (file != null && file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    if (stream.Length > 0 && stream != null)
                    {
                        using (var memorystream = new MemoryStream())
                        {
                            item.Name = file.FileName;
                            item.TypeFiles = file.ContentType;
                            item.IdUser = (int)HttpContext.Session.GetInt32("Id");
                            stream.CopyTo(memorystream);
                            item.Files = memorystream.ToArray();
                            _file.Create(item);
                        }
                    }
                }
            }
            else NotFound("Нет файла"); //throw new Exception("Ошибка файла!");
            return RedirectToAction("Index", new { id = (int)HttpContext.Session.GetInt32("Id") });
        }

        [HttpPost]
        public ActionResult UpdateFile(FileVM item)
        {
            //_file.Delete(item.Id);
            //сначало удалить прошлый, а потом залить новый... а хотя тут же перезатирается
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            if (file != null && file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    if (stream.Length > 0 && stream != null)
                    {
                        using (var memorystream = new MemoryStream())
                        {
                            item.file.Name = file.FileName;
                            item.file.TypeFiles = file.ContentType;
                            stream.CopyTo(memorystream);
                            item.file.Files = memorystream.ToArray();
                            _file.Update(item.file);
                        }
                    }
                    else throw new Exception("Ошибка файла! ");
                }
            }
            else
            {
                _file.Update(item.file);
            }
            return RedirectToAction("Index", new { id = (int)HttpContext.Session.GetInt32("Id") });
        }

        [HttpPost]
        public ActionResult DeleteFile(int id)
        {
            _file.Delete(id);
            return RedirectToAction("Index", new { id = (int)HttpContext.Session.GetInt32("Id") });
        }

        [HttpPost]
        public ActionResult DownloadFile(int id)
        {
            var file = _file.Get(id);
            if(file != null)
            {
                return new FileContentResult(file.Files, file.TypeFiles)
                {
                    FileDownloadName = file.Name
                };
            }
            else
            {
                return NotFound();
            }
        }
    }
}
