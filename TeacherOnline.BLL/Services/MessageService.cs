using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL.Entities;
using TeacherOnline.DAL;
using Microsoft.EntityFrameworkCore;

namespace TeacherOnline.BLL.Services
{
    public class MessageService :IMessage
    {
        AssistantTeachingContext _context;

        public MessageService(AssistantTeachingContext context)
        {
            _context = context;
        }

        public Message Create(Message item)
        {
            _context.Messages.Add(item);
            _context.SaveChanges();
            return Get(u => u.IdChat == item.IdChat && u.Time == item.Time);
        }
        public void Update(Message item)
        {
            var mes = _context.Messages.FirstOrDefault(u => u.Id == item.Id);
            if (mes != null)
            {
                mes.IdAuthor = item.IdAuthor;
                mes.IdChat = item.IdChat;
                mes.Message1 = item.Message1;
                mes.Time = DateTime.Now;
                _context.Messages.Update(mes);
                _context.SaveChanges();
                return;
            }
            throw new Exception("user is not found?");
        }

        public void Delete(int id)
        {
            Message mes = _context.Messages.FirstOrDefault(e => e.Id == id);
            if (mes != null)
            {
                _context.Messages.Remove(mes);
                _context.SaveChanges();
                return;
            }
            throw new Exception("user is not found?");
        }

        public IEnumerable<Message> Find(Func<Message, bool> predicate)
        {
            return _context.Messages.Where(predicate);
        }

        public Message Get(int id)
        {
            return _context.Messages.Include(u=>u.IdChatNavigation).FirstOrDefault(u => u.Id == id);
        }
        public Message Get(Func<Message, bool> predicate)
        {
            return _context.Messages.Include(u=>u.IdChatNavigation).Include(u=> u.IdAuthorNavigation).FirstOrDefault(predicate);
        }

        public IEnumerable<Message> GetAll()
        {
            return _context.Messages;
        }
    }
}
