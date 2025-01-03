namespace SimpleToDoApp.Models;

public class ToDoTask:BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
    public bool? IsDailyRecurring { get; set; }
    public DateTime? DueDate { get; set; }
}
