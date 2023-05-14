namespace TeacherOnline.DAL.Entities;

public partial class Subject
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IdTeacher { get; set; }

    public string About { get; set; } = null!;

    public virtual ICollection<Estimate> Estimates { get; set; } = new List<Estimate>();

    public virtual Profile IdTeacherNavigation { get; set; } = null!;
}
