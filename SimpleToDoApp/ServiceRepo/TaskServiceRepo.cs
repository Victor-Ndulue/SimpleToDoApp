using SimpleToDoApp.Helpers.DTOs.Requests;
using SimpleToDoApp.Helpers.DTOs.Responses;
using SimpleToDoApp.Helpers.ObjectWrapper;
using SimpleToDoApp.IServiceRepo;

namespace SimpleToDoApp.ServiceRepo;

public sealed class TaskServiceRepo : ITaskServiceRepo
{
    public Task<StandardResponse<TaskResponse>> 
        AddTasksAsync
        (ICollection<AddTaskDto> tasks)
    {
        throw new NotImplementedException();
    }
}
