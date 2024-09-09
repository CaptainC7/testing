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

        [HttpGet("GetTemplates/{taskListTemplateId}")]
        public async Task<IActionResult> GetTaskGroupsByTemplateId(int taskListTemplateId)
        {
            var taskGroups = await _taskGroupServices.GetTaskGroupsByTemplateIdAsync(taskListTemplateId);

            if (taskGroups == null || !taskGroups.Any())
            {
                return NotFound();
            }

            return Ok(taskGroups);
        }

        [HttpPost]
        [Route("/CreateTaskGroup")]
        public async Task<ActionResult<TaskGroupDTO>> CreateTaskGroup([FromBody] AddTaskGroupDTO addtaskGroupDTO)
        {
            if (addtaskGroupDTO == null)
            {
                return BadRequest("TaskGroupDTO is null.");
            }

            var result = await _taskGroupServices.AddTaskGroupAsync(addtaskGroupDTO);

            return CreatedAtAction(nameof(CreateTaskGroup), new { id = result.Id }, result);
        }

        [HttpPut]
        [Route("/UpdateGroup")]
        public async Task<IActionResult> UpdateTaskGroup(int id, [FromBody] AddTaskGroupDTO addTaskGroupDto)
        {
            if (addTaskGroupDto == null || id <= 0)
            {
                return BadRequest("Invalid data.");
            }

            var existingGroup = await _taskGroupServices.UpdateTaskGroupAsync(id, addTaskGroupDto);

            if (existingGroup == null)
            {
                return NotFound("Task group not found.");
            }

            return Ok(existingGroup);
        }

        [HttpDelete]
        [Route("DeleteTaskGroup/{id:int}")]
        public async Task<IActionResult> DeleteTaskGroupByID(int id)
        {
            var result = await _taskGroupServices.DeleteTaskGroupByID(id);

            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
