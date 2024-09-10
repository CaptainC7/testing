using ClassLibraryDLL.Models.DTOs;
using ClassLibraryDLL.Services;
using ClassLibraryDLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace testing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskServices _taskServices;

        public TaskController(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        //[HttpGet("GetTasks/{taskGroupId}")]
        //public async Task<IActionResult> GetTasksByTaskGroupIdAsync(int taskGroupId)
        //{
        //    var tasks = await _taskServices.GetTasksByTaskGroupIdAsync(taskGroupId);

        //    if (tasks == null || !tasks.Any())
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tasks);
        //}

        [HttpGet]
        [Route("GetTasksByTaskGroup/{taskGroupId}")]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTasksByTaskGroupId(int taskGroupId)
        {
            var result = await _taskServices.GetTasksByTaskGroupIdAsync(taskGroupId);

            if (result == null || !result.Any())
            {
                return NotFound("No tasks found for the given TaskGroup.");
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("/CreateTask")]
        public async Task<ActionResult<TaskDTO>> CreateTask([FromBody] AddTaskDTO addTaskDTO)
        {
            if (addTaskDTO == null)
            {
                return BadRequest("TaskDTO is null.");
            }

            var result = await _taskServices.AddTaskAsync(addTaskDTO);

            return CreatedAtAction(nameof(CreateTask), new { id = result.ID }, result);
        }

        [HttpPut]
        [Route("/UpdateTask")]
        public async Task<IActionResult> UpdateTaskAsync(int id, [FromBody] AddTaskDTO addTaskDTO)
        {
            if (addTaskDTO == null || id <= 0)
            {
                return BadRequest("Invalid data.");
            }

            var existingGroup = await _taskServices.UpdateTaskAsync(id, addTaskDTO);

            if (existingGroup == null)
            {
                return NotFound("Task group not found.");
            }

            return Ok(existingGroup);
        }

        //[HttpDelete]
        //[Route("DeleteTask/{id:int}")]
        //public async Task<IActionResult> DeleteTaskByID(int id)
        //{
        //    var result = await _taskServices.DeleteTaskByID(id);

        //    if (!result)
        //    {
        //        return NotFound();
        //    }
        //    return Ok();
        //}

        [HttpDelete]
        [Route("DeleteTask/{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _taskServices.DeleteTaskAsync(id);

            if (!result)
            {
                return NotFound(); // Task not found
            }

            return NoContent(); // Successfully deleted
        }
    }
}
