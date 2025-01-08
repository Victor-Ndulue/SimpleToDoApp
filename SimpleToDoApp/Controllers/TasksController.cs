using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using SimpleToDoApp.Helpers.DTOs.Requests;
using SimpleToDoApp.IServiceRepo;
using System.Security.Claims;

namespace SimpleToDoApp.Controllers;

[Authorize]
public class TasksController : BaseController
{
    private readonly ITaskServiceRepo _taskServiceRepo;

    public TasksController
        (ITaskServiceRepo taskServiceRepo)
    {
        _taskServiceRepo = taskServiceRepo;
    }

    [HttpPost("add-tasks")]
    public async Task<IActionResult>
        AddTasksAsync
        ([FromForm]AddTaskDto addTaskDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _taskServiceRepo.AddTasksAsync(addTaskDto, userId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("delete-task")]
    public async Task<IActionResult>
        DeleteTasksAsync
        ([FromQuery] string taskId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _taskServiceRepo.DeleteTasksAsync(taskId, userId);
        return StatusCode(result.StatusCode, result);
    }

    [EnableQuery]
    [HttpGet("get-user-tasks")]
    public async Task<IActionResult>
        GetUserTaskAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _taskServiceRepo.GetUserTaskAsync(userId);
        return StatusCode(200, result);
    }

    [HttpPut("update-task")]
    public async Task<IActionResult>
        UpdateUserTaskAsync
        ([FromForm] UpdateTaskDto updateTaskDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _taskServiceRepo.UpdateUserTaskAsync(updateTaskDto, userId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("update-task-status")]
    public async Task<IActionResult>
        UpdateTaskStatusAsync
        ([FromForm] UpdateTaskStatusDto updateTaskStatusDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _taskServiceRepo.UpdateTaskStatusAsync(updateTaskStatusDto, userId);
        return StatusCode(result.StatusCode, result);
    }
}
