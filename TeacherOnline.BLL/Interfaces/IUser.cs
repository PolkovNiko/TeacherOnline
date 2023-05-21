using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Interfaces
{
    public interface IUser
    {
        void Create(User item);
        void Delete(int id);
        void Update(User item);
        IEnumerable<User> GetAll();
        User Get(int id);
        IEnumerable<User> Find(Func<User, bool> predicate);
    }
}
