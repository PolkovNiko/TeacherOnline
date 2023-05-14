using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL;
using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Services
{
    public class ProfileService : IProfile
    {
        AssistantTeachingContext _context;

        public ProfileService(AssistantTeachingContext context)
        {
            _context = context;
        }

        public void Create(Profile item)
        {
            _context.Profiles.Add(item);
            _context.SaveChanges();
        }
        public void Update(Profile item)
        {
            var Profile = _context.Profiles.FirstOrDefault(u => u.Id == item.Id);
            if (Profile != null)
            {
                Profile.LastName = item.LastName;
                Profile.FirstName = item.FirstName;
                Profile.Otchestvo = item.Otchestvo;
                Profile.Photo = item.Photo;
                Profile.Group = item.Group;
                Profile.About = item.About;
                Profile.Date = item.Date;
                _context.Profiles.Update(Profile);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            Profile Profile = _context.Profiles.FirstOrDefault(e => e.Id == id);
            if (Profile != null)
            {
                _context.Profiles.Remove(Profile);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Profile> Find(Func<Profile, bool> predicate)
        {
            return _context.Profiles.Where(predicate).AsEnumerable();
        }

        public IQueryable<Profile> Get(int id)
        {
            return _context.Profiles.Where(u => u.Id == id).AsQueryable();
        }

        public IEnumerable<Profile> GetAll()
        {
            return _context.Profiles.AsEnumerable();
        }
    }
}
