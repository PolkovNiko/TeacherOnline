using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL.Entities;
using TeacherOnline.DAL;
using File = TeacherOnline.DAL.Entities.File;
using Microsoft.EntityFrameworkCore;

namespace TeacherOnline.BLL.Services
{
    public class FileService : IFile
    {
        AssistantTeachingContext _context;

        public FileService(AssistantTeachingContext context)
        {
            _context = context;
        }

        public void Create(File item)
        {
            _context.Files.Add(item);
            _context.SaveChanges();
        }
        public void Update(File item)
        {
            var File = _context.Files.FirstOrDefault(u => u.Id == item.Id);
            if(item.Files != null)
            {
                if (File != null)
                {
                    File.Name = item.Name;
                    File.TypeFiles = item.TypeFiles;
                    File.Files = item.Files;
                    File.TypeAccess = item.TypeAccess;
                    File.IdUser = item.IdUser;
                    _context.Files.Update(File);
                    _context.SaveChanges();
                    return;
                }
                throw new Exception("fileS is not found?");
            }
            else
            {
                File.Name = item.Name;
                File.TypeFiles = item.TypeFiles;
                File.TypeAccess = item.TypeAccess;
                File.IdUser = item.IdUser;
                _context.Files.Update(File);
                _context.SaveChanges();
                return;
            }
        }

        public void Delete(int id)
        {
            File File = _context.Files.FirstOrDefault(e => e.Id == id);
            if (File != null)
            {
                _context.Files.Remove(File);
                _context.SaveChanges();
                return;
            }
            throw new Exception("fileS is not found?");
        }

        public IEnumerable<File> Find(Func<File, bool> predicate)
        {
            return _context.Files.Where(predicate);
        }

        public File Get(int id)
        {
            return _context.Files.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<File> GetAll()
        {
            return _context.Files.Include(u=>u.IdUserNavigation);
        }
    }
}
