using SimpleToDoApp.Helpers.Enums;
using TaskStatus = SimpleToDoApp.Helpers.Enums.TaskStatus;

namespace SimpleToDoApp.Models;

public class ToDoTask:BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public TaskStatus? TaskStatus { get; set; }
    public TaskRepetitionInterval? RecurringInterval { get; set; }
    public DateTime? DueDate { get; set; }
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
