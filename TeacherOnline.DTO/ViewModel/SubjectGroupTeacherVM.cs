using TeacherOnline.DAL.Entities;

namespace TeacherOnline.DTO.ViewModel
{
    public class SubjectGroupTeacherVM
    {
        public Subject sub { get; set; } = new Subject();
        public IEnumerable<Group> group { get; set; } = new List<Group>();
        public IEnumerable<Profile> teacher { get; set; } = new List<Profile>();
        public IEnumerable<Subject> subs { get; set; } = new List<Subject>();
    }
}
