namespace TeacherOnline.DAL.Entities;

public partial class Message
{
    public int Id { get; set; }

    public int IdAuthor { get; set; }

    public DateTime Time { get; set; }

    public string Message1 { get; set; } = null!;

    public int IdChat { get; set; }

    public virtual Profile IdAuthorNavigation { get; set; } = null!;

    public virtual Chat IdChatNavigation { get; set; } = null!;
}
