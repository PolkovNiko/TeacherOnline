namespace TeacherOnline.DAL.Entities;

public partial class Chat
{
    public int Id { get; set; }

    public int IdUser1 { get; set; }

    public int IdUser2 { get; set; }

    public virtual Profile IdUser1Navigation { get; set; } = null!;

    public virtual Profile IdUser2Navigation { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
