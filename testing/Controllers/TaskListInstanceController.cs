using ClassLibraryDLL.Models.DTOs;
using ClassLibraryDLL.Services;
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

        [HttpPost]
        [Route("CreateTaskListInstance")]
        public async Task<IActionResult> AddTaskListInstanceAsync([FromBody] AddTaskListInstanceDTO addTaskListInstanceDTO)
        {
            await _taskListInstanceServices.AddTaskListInstanceAsync(addTaskListInstanceDTO);
            return Ok(addTaskListInstanceDTO);
        }

        [HttpPut]
        [Route("UpdateTaskListInstance/{id:int}")]
        public async Task<IActionResult> UpdateTaskListInstanceAsync(int id, [FromBody] AddTaskListInstanceDTO addTaskListInstanceDTO)
        {
            await _taskListInstanceServices.UpdateTaskListInstanceAsync(id, addTaskListInstanceDTO);

            return Ok(addTaskListInstanceDTO);
        }

        [HttpGet]
        [Route("GetTaskListInstance/{id:int}")]
        public async Task<IActionResult> GetTaskListInstanceByIDAsync(int id)
        {
            var template = await _taskListInstanceServices.GetTaskListInstanceByIDAsync(id);

            if (template == null)
            {
                return NotFound();
            }

            return Ok(template);
        }

        [HttpDelete]
        [Route("DeleteTaskListInstance/{id:int}")]
        public async Task<IActionResult> DeleteTaskListInstanceByIDAsync(int id)
        {
            var result = await _taskListInstanceServices.DeleteTaskListInstanceByIDAsync(id);

            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
