using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Interfaces
{
    public interface IProfile
    {
        void Create(Profile item, Stream stream);
        void Delete(int id);
        void Update(Profile item, Stream stream);
        List<Profile> GetAll();
        List<Profile> Get(int id);
        List<Profile> Find(Func<Profile, bool> predicate);
    }
}
