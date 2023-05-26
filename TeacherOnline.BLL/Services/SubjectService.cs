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
            //собакнуть айди, поменять применяемое на номер подгруппы
            int index = 0;
            while (true)
            {
                var count = GetAll().Count();
                item.Id = count == 0 ? 1 : count + 1;
                var temp = Get(item.Id);
                if (temp is null)
                {
                    _context.Subjects.Add(item);
                    _context.SaveChanges();
                    return;
                }
                index++;
            }
        }
        public void Update(Subject item)
        {
            var Subject = _context.Subjects.FirstOrDefault(u => u.Id == item.Id);
            if (Subject != null)
            {
                Subject.IdTeacterPract = item.IdTeacterPract;
                Subject.Name = item.Name;
                Subject.PartGroup = item.PartGroup;
                Subject.About = item.About;
                _context.Subjects.Update(Subject);
                _context.SaveChanges();
                return;
            }
            throw new Exception("subject is not found?");
        }

        public void Delete(int id)
        {
            Subject Subject = _context.Subjects.FirstOrDefault(u => u.Id == id);
            if (Subject != null)
            {
                _context.Subjects.Remove(Subject);
                _context.SaveChanges();
                return;
            }
            throw new Exception("subject is not found?");
        }

        public IEnumerable<Subject> Find(Func<Subject, bool> predicate)
        {
            return _context.Subjects.Include(u => u.IdTeacherNavigation).Where(predicate);
        }

        public Subject Get(int id)
        {
            return _context.Subjects.Include(u => u.IdTeacherNavigation).FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Subject> GetAll()
        {
            return _context.Subjects.Include(u => u.IdTeacherNavigation);
        }
    }
}
