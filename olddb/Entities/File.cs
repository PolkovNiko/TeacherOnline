namespace TeacherOnline.DAL.Entities;

public partial class File
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;

    public int IdTeacher { get; set; }

    public virtual Profile IdTeacherNavigation { get; set; } = null!;
}
