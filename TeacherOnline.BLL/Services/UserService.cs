using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL;
using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Services
{
    public class UserService : IUser
    {
        AssistantTeachingContext _context;

        public UserService(AssistantTeachingContext context)
        {
            _context = context;
        }

        public void Create(User item)
        {
            _context.Users.Add(item);
            _context.SaveChanges();
        }
        public void Update(User item)
        {
            var User = _context.Users.FirstOrDefault(u => u.Id == item.Id);
            if (User != null)
            {
                User.Login = item.Login;
                User.Password = item.Password;
                User.Email = item.Email;
                _context.Users.Update(User);
                _context.SaveChanges();
                return;
            }
            throw new Exception("user is not found?");
        }

        public void Delete(int id)
        {
            User User = _context.Users.FirstOrDefault(e => e.Id == id);
            if (User != null)
            {
                _context.Users.Remove(User);
                _context.SaveChanges();
                return;
            }
            throw new Exception("user is not found?");
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _context.Users.Where(predicate);
        }

        public User Get(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetAll()
        {   
            return _context.Users;
        }
    }
}
