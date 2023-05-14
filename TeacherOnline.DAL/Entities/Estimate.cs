namespace TeacherOnline.DAL.Entities;

public partial class Estimate
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public int IdSubject { get; set; }

    public int Score { get; set; }

    public int IdTeacher { get; set; }

    public virtual Subject IdSubjectNavigation { get; set; } = null!;

    public virtual Profile IdTeacherNavigation { get; set; } = null!;

    public virtual Profile IdUserNavigation { get; set; } = null!;
}
