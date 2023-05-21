namespace TeacherOnline.DAL.Entities;

public partial class GroupsInSub
{
    public int Id { get; set; }

    public int IdGroups { get; set; }

    public int IdSubject { get; set; }

    public int IdTeacher { get; set; }

    public virtual Group IdGroupsNavigation { get; set; } = null!;

    public virtual Subject IdSubjectNavigation { get; set; } = null!;

    public virtual Profile IdTeacherNavigation { get; set; } = null!;
}
