namespace SimpleToDoApp.Models;

public class AppUser:BaseEntity
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? PasswordSalt { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public virtual ICollection<ToDoTask>? ToDoTasks { get; set; }
}