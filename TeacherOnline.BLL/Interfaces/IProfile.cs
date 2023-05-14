using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Interfaces
{
    public interface IProfile
    {
        void Create(Profile item);
        void Delete(int id);
        void Update(Profile item);
        IEnumerable<Profile> GetAll();
        IQueryable<Profile> Get(int id);
        IEnumerable<Profile> Find(Func<Profile, bool> predicate);
    }
}
