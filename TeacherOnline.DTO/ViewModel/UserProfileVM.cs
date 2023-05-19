using TeacherOnline.DAL.Entities;
using TeacherOnline.DTO.ModelsDTO;
namespace TeacherOnline.DTO.ViewModel
{
    public class UserProfileVM
    {
        public Profile Profile {get; set; } = new Profile();
        public IEnumerable<Group> Group { get; set; } = new List<Group>();
    }
}
