namespace SimpleToDoApp.Helpers.DTOs.Requests;

public record AddTaskDto
(
    string title, string? description,
    bool isDailyRecurring, DateOnly dueDate,
    TimeOnly dueTime
);