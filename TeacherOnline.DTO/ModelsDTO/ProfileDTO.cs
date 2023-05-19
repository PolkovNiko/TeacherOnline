namespace TeacherOnline.DTO.ModelsDTO;

public partial class ProfileDTO
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Otchestvo { get; set; } = null!;

    public DateTime Date { get; set; }

    public int? Groups { get; set; }

    public byte[] Photo { get; set; } = null!;

    public string About { get; set; } = null!;

}
