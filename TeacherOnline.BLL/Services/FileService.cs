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
            if (File != null)
            {
                if(item.TypeAccess == 1)
                {
                    if(item.Files != null)
                    {
                            File.Name = item.Name;
                            File.TypeFiles = item.TypeFiles;
                            File.Files = item.Files;
                            File.TypeAccess = item.TypeAccess;
                            File.IdSub = item.IdSub;
                            _context.Files.Update(File);
                            _context.SaveChanges();
                            return;
                    }
                    else
                    {
                        File.TypeAccess = item.TypeAccess;
                        File.IdSub = item.IdSub;
                        _context.Files.Update(File);
                        _context.SaveChanges();
                        return;
                    }

                }
                else
                {
                    if(item.Files != null)
                    {
                        File.Files = item.Files;
                        File.TypeFiles = item.TypeFiles;
                        File.Name = item.Name;
                        File.TypeAccess = item.TypeAccess;
                        File.IdSub = 0;
                        _context.Files.Update(File);
                        _context.SaveChanges();
                        return;
                    }
                    else
                    {
                        File.TypeAccess = item.TypeAccess;
                        File.IdSub = 0;
                        _context.Files.Update(File);
                        _context.SaveChanges();
                        return;
                    }
                }
            }
            throw new Exception("fileS is not found?");
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
