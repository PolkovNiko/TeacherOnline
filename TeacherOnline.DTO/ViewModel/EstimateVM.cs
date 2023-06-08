using TeacherOnline.DAL.Entities;

namespace TeacherOnline.DTO.ViewModel
{
    public class EstimateVM
    {
        public Estimate estimate { get; set; } = new Estimate();
        //public IEnumerable<Profile> teacher { get; set; } = new List<Profile>();
        public IEnumerable<Profile> users { get; set; } = new List<Profile>();
        public IEnumerable<Subject> subjects { get; set; } = new List<Subject>();
        public Subject subject { get; set; } = new Subject();
        public IEnumerable<Estimate> estimateList { get; set; } = new List<Estimate>();
        public IEnumerable<Group> groups { get; set; } = new List<Group>();
        public Group OneGroup { get; set; } = new Group();
        public Profile user { get; set; } = new Profile();
        public Filters Fltr { get; set; }
        public Sorted Str { get; set; }

        public class Filters
        {
            public Filters(int selectedSubject, int selectedUser)
            {
                SelectedSubject = selectedSubject;
                SelectedUser = selectedUser;
            }
            public int SelectedSubject { get; set; }
            public int SelectedUser { get; set; }
        }
        public class Sorted
        {
            public Sorted(SortStateEstimate sortOrder)
            {
                IdSubSort = sortOrder == SortStateEstimate.IdSubAsc ? SortStateEstimate.IdSubDesc : SortStateEstimate.IdSubAsc;
                ScoreSort = sortOrder == SortStateEstimate.ScoreAsc ? SortStateEstimate.ScoreDesc : SortStateEstimate.ScoreAsc;
                DataAddSort = sortOrder == SortStateEstimate.DateAddAsc ? SortStateEstimate.DateAddDesc : SortStateEstimate.DateAddAsc;
                Current = sortOrder;
            }
            public SortStateEstimate IdSubSort { get; set; }
            public SortStateEstimate ScoreSort { get; set; }
            public SortStateEstimate DataAddSort { get; set; }
            public SortStateEstimate Current { get; set; }
        }

        public enum SortStateEstimate
        {
            IdSubAsc,
            IdSubDesc,
            ScoreAsc,
            ScoreDesc,
            DateAddAsc,
            DateAddDesc
        } 

        public Dictionary<int, int> CountEstTeory = new Dictionary<int, int>();
        public Dictionary<int, int> CountEstPrakt = new Dictionary<int, int>();
        public Dictionary<int, int> CountEstOKR = new Dictionary<int, int>();

        public Dictionary<int, string> TypeEstimate = new Dictionary<int, string>
        {
            {0, "Теория" },
            {1, "Практика" },
            {2, "ОКР" }
        };

        public Dictionary<int, double> AvgEstimate = new Dictionary<int, double>();
        public Dictionary<int, string> DisplaySubj = new Dictionary<int, string>();
        public Dictionary<int, string> DisplayUser = new Dictionary<int, string>();
    }
}
