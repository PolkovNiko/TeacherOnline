using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Interfaces
{
    public interface IMessage
    {
        Message Create(Message item);
        void Delete(int id);
        void Update(Message item);
        IEnumerable<Message> GetAll();
        Message Get(int id);
        Message Get(Func<Message, bool> predicate);
        IEnumerable<Message> Find(Func<Message, bool> predicate);
    }
}
