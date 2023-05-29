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
    }
}
