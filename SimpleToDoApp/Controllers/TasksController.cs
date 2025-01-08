using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SimpleToDoApp.Helpers.DTOs.Requests;
using SimpleToDoApp.IServiceRepo;
using System.Security.Claims;

namespace SimpleToDoApp.Controllers;

public class TasksController : BaseController
{
    private readonly ITaskServiceRepo _taskServiceRepo;

    public TasksController(ITaskServiceRepo taskServiceRepo)
    {
        _taskServiceRepo = taskServiceRepo;
    }

    [HttpPost("add-tasks")]
    public async Task<IActionResult>
        AddTasksAsync(ICollection<AddTaskDto> tasks)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _taskServiceRepo.AddTasksAsync(tasks, userId);
        return StatusCode(result.StatusCode, result);
    }
}
