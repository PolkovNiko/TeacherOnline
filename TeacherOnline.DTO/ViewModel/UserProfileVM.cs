using TeacherOnline.DAL.Entities;
namespace TeacherOnline.DTO.ViewModel
{
    public class UserProfileVM
    {
        public Profile Profile {get; set; } = new Profile();
        public User user { get; set; } = new User();
        public IEnumerable<Group> Group { get; set; } = new List<Group>();
        public List<string> roles = new List<string>();

        public IEnumerable<Profile> profileList { get; set; } = new List<Profile>();
        public Filters Fltr { get; set; }
        public Sorted Str { get; set; }

        //-----------------\
        public class Filters
        {
            public Filters(string selectedLastName, string selectedFisrtName, string selectedOtchestvo, string selectedRoles)
            {
                SelectedLastName = selectedLastName;
                SelectedFisrtName = selectedFisrtName;
                SelectedOtchestvo = selectedOtchestvo;
                SelectedRoles = selectedRoles;
            }

            public string SelectedLastName { get; set; }
            public string SelectedFisrtName{ get; set; }
            public string SelectedOtchestvo { get; set; }
            public string SelectedRoles { get; set; }
        }
        public class Sorted
        {
            public Sorted(SortStateProfile sortOrder)
            {
                LastNameSort = sortOrder == SortStateProfile.LastNameAsc ? SortStateProfile.LastNameDesc : SortStateProfile.LastNameAsc;
                FirstNameSort = sortOrder == SortStateProfile.FirstNameAsc ? SortStateProfile.FirstNameDesc : SortStateProfile.FirstNameAsc;
                OtchestvoSort = sortOrder == SortStateProfile.OtchestvoAsc ? SortStateProfile.OtchestvoDesc : SortStateProfile.OtchestvoAsc;
                GroupsSort = sortOrder == SortStateProfile.GroupsAsc ? SortStateProfile.GroupsDesc : SortStateProfile.GroupsAsc;
                Current = sortOrder;
            }
            public SortStateProfile LastNameSort { get; set; }
            public SortStateProfile FirstNameSort { get; set; }
            public SortStateProfile OtchestvoSort { get; set; }
            public SortStateProfile GroupsSort { get; set; }
            public SortStateProfile Current { get;  set; }
        }


        public enum SortStateProfile
        {
            LastNameAsc,
            LastNameDesc,
            FirstNameAsc,
            FirstNameDesc,
            OtchestvoAsc,
            OtchestvoDesc,
            GroupsAsc,
            GroupsDesc,
        }
    }
}
