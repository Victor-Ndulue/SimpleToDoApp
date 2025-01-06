namespace SimpleToDoApp.Models;

public class BaseEntity
{
    public string? Id { get; set; } = Ulid.NewUlid().ToString();
    public string? CreatedBy { get; set; }
    public DateTime? DateCreated { get; set; } = DateTime.UtcNow;
    public string? UpdatedBy { get; set; }
    public DateTime? DateUpdated { get; set; }
    public bool? IsDeleted { get; set; }
}
