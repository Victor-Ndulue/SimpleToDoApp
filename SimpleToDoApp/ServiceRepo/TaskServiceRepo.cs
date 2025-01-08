using SimpleToDoApp.Helpers.DTOs.Requests;
using SimpleToDoApp.Helpers.DTOs.Responses;
using SimpleToDoApp.Helpers.ObjectWrapper;
using SimpleToDoApp.IServiceRepo;

namespace SimpleToDoApp.ServiceRepo;

public sealed class TaskServiceRepo : ITaskServiceRepo
{
    public Task<StandardResponse<TaskResponse>> 
        AddTasksAsync
        (ICollection<AddTaskDto> tasks, string? userId)
    {
        throw new NotImplementedException();
    }

    public Task<StandardResponse<TaskResponse>> 
        DeleteTasksAsync
        (string taskId, string? userId)
    {
        throw new NotImplementedException();
    }

    public Task<StandardResponse<IQueryable<TaskResponse>>> 
        GetUserTask
        (string? userId)
    {
        throw new NotImplementedException();
    }

    public Task<StandardResponse<string>> 
        UpdateUserTaskAsync
        (UpdateTaskDto updateTaskDto, string? userId)
    {
        throw new NotImplementedException();
    }
}
