using SimpleToDoApp.Helpers.DTOs.Requests;
using SimpleToDoApp.Helpers.DTOs.Responses;
using SimpleToDoApp.Helpers.ObjectWrapper;

namespace SimpleToDoApp.IServiceRepo;

public interface ITaskServiceRepo
{
    Task<StandardResponse<TaskResponse>>
        AddTasksAsync(ICollection<AddTaskDto> tasks, string? userId);

    Task<StandardResponse<TaskResponse>> 
        DeleteTasksAsync
        (string taskId, string? userId);

    Task<StandardResponse<IQueryable<TaskResponse>>>
        GetUserTask(string? userId);

    Task<StandardResponse<string>>
        UpdateUserTaskAsync(UpdateTaskDto updateTaskDto, string? userId);
}