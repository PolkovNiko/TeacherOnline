using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Interfaces
{
    public interface IProfile
    {
        void Create(Profile item, Stream stream);
        void Delete(int id);
        void Update(Profile item, Stream stream);
        IEnumerable<Profile> GetAll();
        Profile Get(int id);
        IEnumerable<Profile> Find(Func<Profile, bool> predicate);
    }
}
