using ClassLibraryDLL.Models.DTOs;
using ClassLibraryDLL.Services;
using ClassLibraryDLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace testing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskGroupController : ControllerBase
    {
        private readonly ITaskGroupServices _taskGroupServices;

        public TaskGroupController(ITaskGroupServices taskGroupServices)
        {
            _taskGroupServices = taskGroupServices;
        }

        [HttpGet]
        [Route("GetTaskGroupsByTemplate/{taskListTemplateId}")]
        public async Task<ActionResult<IEnumerable<TaskGroupDTO>>> GetTaskGroupsByTemplateId(int taskListTemplateId)
        {
            var result = await _taskGroupServices.GetTaskGroupsByTemplateIdAsync(taskListTemplateId);

            if (result == null || !result.Any())
            {
                return NotFound("No task groups found for the given TaskListTemplate.");
            }

            return Ok(result);
        }


        [HttpPost]
        [Route("/CreateTaskGroup")]
        public async Task<ActionResult<TaskGroupDTO>> CreateTaskGroup([FromBody] AddTaskGroupDTO addtaskGroupDTO, int userID)
        {
            if (addtaskGroupDTO == null)
            {
                return BadRequest("TaskGroupDTO is null.");
            }

            var result = await _taskGroupServices.AddTaskGroupAsync(addtaskGroupDTO, userID);

            return CreatedAtAction(nameof(CreateTaskGroup), new { id = result.Id }, result);
        }

        [HttpPut]
        [Route("/UpdateGroup")]
        public async Task<IActionResult> UpdateTaskGroupAsync(int id, [FromBody] AddTaskGroupDTO addTaskGroupDto, int userID)
        {
            if (addTaskGroupDto == null || id <= 0)
            {
                return BadRequest("Invalid data.");
            }

            var existingGroup = await _taskGroupServices.UpdateTaskGroupAsync(id, addTaskGroupDto, userID);

            if (existingGroup == null)
            {
                return NotFound("Task group not found.");
            }

            return Ok(existingGroup);
        }

        [HttpDelete]
        [Route("DeleteTaskGroup/{id:int}")]
        public async Task<IActionResult> DeleteTaskGroupAsync(int id, int userID)
        {
            var result = await _taskGroupServices.DeleteTaskGroupAsync(id, userID);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
