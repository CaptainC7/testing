using ClassLibraryDLL.Models.DTOs;
using ClassLibraryDLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace testing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskListInstanceController : ControllerBase
    {
        private readonly ITaskListInstanceServices _taskListInstanceServices;

        public TaskListInstanceController(ITaskListInstanceServices taskListInstanceServices)
        {
            _taskListInstanceServices = taskListInstanceServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskListInstanceDTO>>> GetAllTaskListInstances()
        {
            var result = await _taskListInstanceServices.GetAllTaskListInstancesAsync();
            return Ok(result);
        }
    }
}
