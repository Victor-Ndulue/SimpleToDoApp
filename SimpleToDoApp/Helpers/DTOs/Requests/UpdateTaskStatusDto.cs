namespace SimpleToDoApp.Helpers.DTOs.Requests;
using TaskStatus = SimpleToDoApp.Helpers.Enums.TaskStatus;
public record UpdateTaskStatusDto
(
    string taskId, TaskStatus taskStatus
);
