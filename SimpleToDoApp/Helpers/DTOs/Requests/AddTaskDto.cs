using SimpleToDoApp.Helpers.Enums;

namespace SimpleToDoApp.Helpers.DTOs.Requests;

public record AddTaskDto
(
    string title, string? description,
    TaskRepetitionInterval recurrence, 
    DateTime? dueDateTime
);