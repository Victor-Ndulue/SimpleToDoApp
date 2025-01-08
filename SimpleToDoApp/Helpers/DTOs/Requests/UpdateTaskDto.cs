using SimpleToDoApp.Helpers.Enums;
using TaskStatus = SimpleToDoApp.Helpers.Enums.TaskStatus;

namespace SimpleToDoApp.Helpers.DTOs.Requests;

public record UpdateTaskDto
(
    string taskId, string title, 
    string? description, TaskRepetitionInterval recurrence,
    TaskStatus? taskStatus, DateTime? dueDateTime
);
