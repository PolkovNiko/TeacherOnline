namespace TeacherOnline.DAL.Entities;

public partial class Group
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Specialty { get; set; } = null!;

    public virtual ICollection<GroupsInSub> GroupsInSubs { get; set; } = new List<GroupsInSub>();
}
