using ClassLibraryDLL.Services;
using ClassLibraryDLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace testing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskInstanceController : ControllerBase
    {
        private readonly ITaskInstanceServices _taskInstanceServices;

        public TaskInstanceController(ITaskInstanceServices taskInstanceController)
        {
            _taskInstanceServices = taskInstanceController;
        }

        [HttpGet("taskgroupinstance/{taskGroupInstanceId}")]
        public async Task<IActionResult> GetTaskInstancebyTaskGroupInstanceID(int taskGroupInstanceId)
        {
            try
            {
                var taskInstances = await _taskInstanceServices.GetTaskInstancebyTaskGroupInstanceID(taskGroupInstanceId);

                if (taskInstances == null || !taskInstances.Any())
                {
                    return NotFound("No task instances found for the given TaskGroupInstanceID.");
                }

                return Ok(taskInstances);
            }
            catch (Exception ex)
            {
                // Log the error (you can add logging here)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
