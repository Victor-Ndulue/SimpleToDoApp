namespace SimpleToDoApp.Helpers.DTOs.Responses;

public record TaskResponse
(
    string taskId, string title, 
    string? description, bool isDailyRecurring, 
    DateOnly dueDate, TimeOnly dueTime
);