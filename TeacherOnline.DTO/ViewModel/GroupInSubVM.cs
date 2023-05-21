using TeacherOnline.DAL.Entities;

namespace TeacherOnline.DTO.ViewModel
{
    public class GroupInSubVM
    {
        public GroupsInSub groupsInSub{ get; set; } = new GroupsInSub();
        public IEnumerable<GroupsInSub> gisList { get; set; } = new List<GroupsInSub>();
        public IEnumerable<Group> groups { get; set; } = new List<Group>();
        public IEnumerable<Subject> subjects { get; set; } = new List<Subject>();
    }
}
