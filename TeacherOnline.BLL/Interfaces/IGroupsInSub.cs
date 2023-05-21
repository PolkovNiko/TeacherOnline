using TeacherOnline.DAL.Entities;

namespace TeacherOnline.BLL.Interfaces
{
    public interface IGroupsInSub
    {
        void Create(GroupsInSub item);
        void Delete(int id);
        void Update(GroupsInSub item);
        IEnumerable<GroupsInSub> GetAll();
        GroupsInSub Get(int id);
        IEnumerable<GroupsInSub> Find(Func<GroupsInSub, bool> predicate);
    }
}
