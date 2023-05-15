using Microsoft.EntityFrameworkCore;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL;
using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Services
{
    public class SubjectService : ISubject
    {
        AssistantTeachingContext _context;

        public SubjectService(AssistantTeachingContext context)
        {
            _context = context;
        }

        public void Create(Subject item)
        {
            _context.Subjects.Add(item);
            _context.SaveChanges();
        }
        public void Update(Subject item)
        {
            var Subject = _context.Subjects.FirstOrDefault(u => u.Id == item.Id);
            if (Subject != null)
            {
                Subject.IdTeacher = item.IdTeacher;
                Subject.Name = item.Name;
                Subject.About = item.About;
                _context.Subjects.Update(Subject);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            Subject Subject = _context.Subjects.FirstOrDefault(u => u.Id == id);
            if (Subject != null)
            {
                _context.Subjects.Remove(Subject);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Subject> Find(Func<Subject, bool> predicate)
        {
            return _context.Subjects.Include(u => u.IdTeacherNavigation).Where(predicate).AsEnumerable();
        }

        public IQueryable<Subject> Get(int id)
        {
            return _context.Subjects.Include(u => u.IdTeacherNavigation).Where(u => u.Id == id).AsQueryable();
        }

        public IEnumerable<Subject> GetAll()
        {
            return _context.Subjects.Include(u => u.IdTeacherNavigation).AsEnumerable();
        }
    }
}
