using Microsoft.AspNetCore.Mvc;
using SimpleToDoApp.Helpers.DTOs.Requests;
using SimpleToDoApp.IServiceRepo;

namespace SimpleToDoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleToDoAppController : ControllerBase
    {
        private readonly ITaskServiceRepo _taskServiceRepo;

        public SimpleToDoAppController(ITaskServiceRepo taskServiceRepo)
        {
            _taskServiceRepo = taskServiceRepo;
        }

        [HttpPost("add-tasks")]
        public async Task<IActionResult>
            AddTasksAsync(ICollection<AddTaskDto> tasks)
        {
            var result = await _taskServiceRepo.AddTasksAsync(tasks);
            return StatusCode(result.StatusCode, result);
        }
    }
}
