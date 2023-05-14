using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Interfaces
{
    public interface IGroup
    {
        void Create(Group item);
        void Delete(int id);
        void Update(Group item);
        IEnumerable<Group> GetAll();
        IQueryable<Group> Get(int id);
        IEnumerable<Group> Find(Func<Group, bool> predicate);
    }
}
