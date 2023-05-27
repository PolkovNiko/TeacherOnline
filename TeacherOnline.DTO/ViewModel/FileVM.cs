using TeacherOnline.DAL.Entities;

namespace TeacherOnline.DTO.ViewModel
{
    public class FileVM
    {
        public DAL.Entities.File file { get; set; } = new DAL.Entities.File();
        public List<DAL.Entities.File> files { get; set; } = new List<DAL.Entities.File>();
        public List<GroupsInSub> groupsInSubs { get; set; } = new List<GroupsInSub>();
        public Subject subject { get; set; } = new Subject();
        public List<Subject> subjects { get; set; } = new List<Subject>();
        public List<Profile> users { get; set; } = new List<Profile>();
        public Profile user { get; set; } = new Profile();
        public Dictionary<int, string> TypeAccesses = new Dictionary<int, string>
        {
            {0, "Только мне" },
            {1, "Только предмету" },
            {2, "Всем" }
        };
    }
}
