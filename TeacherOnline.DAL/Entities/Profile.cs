namespace TeacherOnline.DAL.Entities;

public partial class Profile
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Otchestvo { get; set; } = null!;

    public DateTime Date { get; set; }

    public string? Group { get; set; }

    public byte[] Photo { get; set; } = null!;

    public string About { get; set; } = null!;

    public virtual ICollection<Estimate> EstimateIdTeacherNavigations { get; set; } = new List<Estimate>();

    public virtual ICollection<Estimate> EstimateIdUserNavigations { get; set; } = new List<Estimate>();

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual User IdNavigation { get; set; } = null!;

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
