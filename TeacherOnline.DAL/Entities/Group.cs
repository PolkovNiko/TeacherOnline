namespace TeacherOnline.DAL.Entities;

public partial class Group
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Specialty { get; set; } = null!;
}
