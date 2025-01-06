using SimpleToDoApp.Helpers.Enums;
using TaskStatus = SimpleToDoApp.Helpers.Enums.TaskStatus;

namespace SimpleToDoApp.Helpers.DTOs.Responses;

public record TaskResponse
(
    string taskId, string title, 
    string? description, TaskStatus taskStatus,
    TaskRepetitionInterval recurrence, 
    DateOnly dueDate, TimeOnly dueTime
);