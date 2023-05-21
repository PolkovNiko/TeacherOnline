using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Interfaces
{
    public interface ISubject
    {
        void Create(Subject item);
        void Delete(int id);
        void Update(Subject item);
        IEnumerable<Subject> GetAll();
        Subject Get(int id);
        IEnumerable<Subject> Find(Func<Subject, bool> predicate);
    }
}
