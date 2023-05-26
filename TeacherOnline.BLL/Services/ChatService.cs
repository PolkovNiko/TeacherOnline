using Microsoft.EntityFrameworkCore;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL;
using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Services
{
    public class ChatService : IChat
    {
        AssistantTeachingContext _context;

        public ChatService(AssistantTeachingContext context)
        {
            _context = context;
        }

        public int Create(Chat item)
        {
            _context.Chats.Add(item);
            _context.SaveChanges();
            return Get(u=> u.IdUser1 == item.IdUser1 && u.IdUser2 == item.IdUser2).Id;
        }
        public void Update(Chat item)
        {
            var chat = _context.Chats.FirstOrDefault(u => u.Id == item.Id);
            if (chat != null)
            {
                chat.IdUser1 = item.IdUser1;
                chat.IdUser2 = item.IdUser2;
                _context.Chats.Update(chat);
                _context.SaveChanges();
                return;
            }
            throw new Exception("user is not found?");
        }

        public void Delete(int id)
        {
            Chat chat = _context.Chats.FirstOrDefault(e => e.Id == id);
            if (chat != null)
            {
                _context.Chats.Remove(chat);
                _context.SaveChanges();
                return;
            }
            throw new Exception("user is not found?");
        }

        public IEnumerable<Chat> Find(Func<Chat, bool> predicate)
        {
            return _context.Chats.Include(u=> u.Messages).Include(u => u.IdUser1Navigation).Include(u => u.IdUser2Navigation).Where(predicate);
        }

        public Chat Get(int id)
        {
            return _context.Chats.Include(u => u.IdUser1Navigation).ThenInclude(u=> u.IdNavigation)
                .Include(u => u.IdUser2Navigation).ThenInclude(u => u.IdNavigation).Include(u => u.Messages).FirstOrDefault(u => u.Id == id);
        }
        public Chat Get(Func<Chat, bool> predicate)
        {
            return _context.Chats.Include(u => u.IdUser1Navigation)
                .Include(u => u.IdUser2Navigation)
                .Include(u => u.Messages)
                    .ThenInclude(x=> x.IdAuthorNavigation)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<Chat> GetAll()
        {
            return _context.Chats.Include(u => u.IdUser1Navigation)
                .Include(u => u.IdUser2Navigation).Include(u => u.Messages);
        }

    }
}
