namespace TeacherOnline.DAL.Entities;

public partial class File
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string TypeFiles { get; set; } = null!;

    public byte[] Files { get; set; } = null!;

    public int TypeAccess { get; set; }

    public int IdUser { get; set; }

    public int IdSub { get; set; }

    public virtual Profile IdUserNavigation { get; set; } = null!;
}
