using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Interfaces
{
    public interface IChat
    {
        int Create(Chat item);
        void Delete(int id);
        void Update(Chat item);
        IEnumerable<Chat> GetAll();
        Chat Get(int id);
        Chat Get(Func<Chat, bool> predicate);
        IEnumerable<Chat> Find(Func<Chat, bool> predicate);
    }
}
