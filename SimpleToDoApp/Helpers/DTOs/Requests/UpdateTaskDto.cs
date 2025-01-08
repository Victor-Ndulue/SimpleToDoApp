using SimpleToDoApp.Helpers.Enums;

namespace SimpleToDoApp.Helpers.DTOs.Requests;

public record UpdateTaskDto
(
    string? taskId, string? title, 
    string? description, TaskRepetitionInterval recurrence,
    DateTime? dueDateTime
);
