namespace SimpleToDoApp.Models;

public class BaseEntity
{
    public string? Id { get; set; } = Ulid.NewUlid().ToString();
    public DateTime? DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime? DateUpdated { get; set; }
    public bool? IsDeleted { get; set; }
}
