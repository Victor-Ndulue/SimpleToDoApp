using Microsoft.EntityFrameworkCore;
using SimpleToDoApp.DataAccess.DataContext;
using SimpleToDoApp.Helpers.DTOs.Requests;
using SimpleToDoApp.Helpers.DTOs.Responses;
using SimpleToDoApp.Helpers.ObjectWrapper;
using SimpleToDoApp.IServiceRepo;
using SimpleToDoApp.Models;
using TaskStatus = SimpleToDoApp.Helpers.Enums.TaskStatus;

namespace SimpleToDoApp.ServiceRepo;

public sealed class TaskServiceRepo : ITaskServiceRepo
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<ToDoTask> _toDoTasks;

    public TaskServiceRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _toDoTasks = dbContext.ToDoTasks;
    }

    public async Task<StandardResponse<string>>
        AddTasksAsync
        (AddTaskDto addTaskDto, string? userId)
    {
        var toDoTasks =  new ToDoTask
        {
            Title = addTaskDto.title,
            Description = addTaskDto.description,
            TaskStatus = TaskStatus.Pending,
            RecurringInterval = addTaskDto.recurrence,
            DueDate = addTaskDto.dueDateTime,
            AppUserId = userId,
            CreatedBy = userId
        };

        await _toDoTasks.AddAsync(toDoTasks);
        await _dbContext.SaveChangesAsync();
        string successMsg = "Task added successfully";
        return StandardResponse<string>.Success(data: successMsg, statusCode: 201);
    }

    public async Task<StandardResponse<string>>
        DeleteTasksAsync
        (string taskId, string? userId)
    {
        var taskToDelete = await _toDoTasks
            .Where(task => task.Id == taskId && task.AppUserId == userId)
            .FirstOrDefaultAsync();
        if (taskToDelete == null)
        {
            string errorMsg = "Task not found";
            return StandardResponse<string>.Failed(data: null, errorMessage: errorMsg);
        }
        _toDoTasks.Remove(taskToDelete);
        await _dbContext.SaveChangesAsync();
        string successMsg = "Task deleted successfully";
        return StandardResponse<string>.Success(data: successMsg, statusCode: 200);
    }

    public async Task<IEnumerable<TaskResponse>>
        GetUserTaskAsync
        (string? userId)
    {
        var tasks = await _toDoTasks.Where(task => task.AppUserId == userId)
            .AsNoTracking().ToListAsync();
        return tasks
            .Select(task => new TaskResponse
            (
                taskId: task.Id,
                title: task.Title,
                description: task.Description,
                taskStatus: task.TaskStatus,
                recurrence: task.RecurringInterval,
                dueDateTime: task.DueDate
            ));
    }

    public async Task<StandardResponse<string>>
        UpdateUserTaskAsync
        (UpdateTaskDto updateTaskDto, string? userId)
    {
        var tasks = await _toDoTasks
            .Where(task => task.Id == updateTaskDto.taskId && task.AppUserId == userId)
            .FirstOrDefaultAsync();
        if (tasks is null)
        {
            string errorMsg = "Task not found";
            return StandardResponse<string>.Failed(data: null, errorMessage: errorMsg);
        }
        UpdateTaskProperties(tasks, updateTaskDto);
        await _dbContext.SaveChangesAsync();
        string successMsg = "Task updated successfully";
        return StandardResponse<string>.Success(data: successMsg);
    }

    public async Task<StandardResponse<string>>
        UpdateTaskStatusAsync
        (UpdateTaskStatusDto updateTaskDto, string? userId)
    {
        var taskToUpdate = await _toDoTasks
            .Where(task => task.Id == updateTaskDto.taskId && task.AppUserId == userId).FirstOrDefaultAsync();
        if (taskToUpdate is null)
        {
            string errorMsg = "Task not found";
            return StandardResponse<string>.Failed(data: null, errorMessage: errorMsg);
        }
        taskToUpdate.TaskStatus = updateTaskDto.taskStatus;
        await _dbContext.SaveChangesAsync();

        string successMsg = "Task status updated successfully";
        return StandardResponse<string>.Success(data: successMsg);
    }

    private void UpdateTaskProperties
        (ToDoTask task, UpdateTaskDto dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.title))
        {
            task.Title = dto.title;
        }
        task.Description = dto.description;
        task.RecurringInterval = dto.recurrence;
        task.DueDate = dto.dueDateTime;
    }
}
