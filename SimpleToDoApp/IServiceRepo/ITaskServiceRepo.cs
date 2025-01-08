using SimpleToDoApp.Helpers.DTOs.Requests;
using SimpleToDoApp.Helpers.DTOs.Responses;
using SimpleToDoApp.Helpers.ObjectWrapper;

namespace SimpleToDoApp.IServiceRepo;

public interface ITaskServiceRepo
{
    Task<StandardResponse<string>>
        AddTasksAsync(AddTaskDto addTaskDto, string? userId);

    Task<StandardResponse<string>>
        DeleteTasksAsync
        (string taskId, string? userId);

    Task<IEnumerable<TaskResponse>>
        GetUserTaskAsync
        (string? userId);

    Task<StandardResponse<string>>
        UpdateUserTaskAsync
        (UpdateTaskDto updateTaskDto, string? userId);

    Task<StandardResponse<string>>
        UpdateTaskStatusAsync
        (UpdateTaskStatusDto updateTaskDto, string? userId);
}