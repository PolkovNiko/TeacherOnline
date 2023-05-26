using Microsoft.EntityFrameworkCore;
using TeacherOnline.BLL.Interfaces;
using TeacherOnline.DAL;
using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Services
{
    public class GroupsInSubService : IGroupsInSub
    {
        AssistantTeachingContext _context;

        public GroupsInSubService(AssistantTeachingContext context)
        {
            _context = context;
        }

        public void Create(GroupsInSub item)
        {
            int index = 0;
            while (true)
            {
                var count = GetAll().Count();
                item.Id = count == 0 ? 1 : count + 1 + index;
                var temp = Get(item.Id);
                if (temp is null)
                {
                    _context.GroupsInSubs.Add(item);
                    _context.SaveChanges();
                    return;
                }
                index++;
            }
        }
        public void Update(GroupsInSub item)
        {
            var GroupInSub = _context.GroupsInSubs.FirstOrDefault(u => u.Id == item.Id);
            if (GroupInSub != null)
            {
                GroupInSub.IdGroups = item.IdGroups;
                GroupInSub.IdSubject = item.IdSubject;
                _context.GroupsInSubs.Update(GroupInSub);
                _context.SaveChanges();
                return;
            }
            throw new Exception("groupsInSub is not found?");
        }

        public void Delete(int id)
        {
            var GroupInSub = _context.GroupsInSubs.FirstOrDefault(e => e.Id == id);
            if (GroupInSub != null)
            {
                _context.GroupsInSubs.Remove(GroupInSub);
                _context.SaveChanges();
                return;
            }
            throw new Exception("groupsInSub is not found?");
        }

        public IEnumerable<GroupsInSub> Find(Func<GroupsInSub, bool> predicate)
        {
            return _context.GroupsInSubs.Include(u=> u.IdGroupsNavigation).Include(x => x.IdSubjectNavigation).Where(predicate);
        }

        public GroupsInSub Get(int id)
        {
            return _context.GroupsInSubs.FirstOrDefault(u => u.Id == id);
        }
        
        public GroupsInSub Get(Func<GroupsInSub, bool> predicate)
        {
            return _context.GroupsInSubs.Include(u => u.IdGroupsNavigation).FirstOrDefault(predicate);
        }

        public IEnumerable<GroupsInSub> GetAll()
        {
            return _context.GroupsInSubs.Include(u=> u.IdGroupsNavigation).Include(x=> x.IdSubjectNavigation);
        }
    }
}
