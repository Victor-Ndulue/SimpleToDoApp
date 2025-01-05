using SimpleToDoApp.Helpers.DTOs.Requests;
using SimpleToDoApp.Helpers.DTOs.Responses;
using SimpleToDoApp.Helpers.ObjectWrapper;

namespace SimpleToDoApp.IServiceRepo;

public interface ITaskServiceRepo
{
    Task<StandardResponse<TaskResponse>>
        AddTasksAsync(ICollection<AddTaskDto> tasks);
}