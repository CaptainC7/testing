using ClassLibraryDLL.Models.DTOs;
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

        [HttpGet("TaskInstance/{taskGroupInstanceId}")]
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
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("/CreateTaskInstance")]
        public async Task<ActionResult<TaskGroupInstanceDTO>> AddTaskInstance([FromBody] AddTaskInstanceDTO addtaskInstanceDTO)
        {
            if (addtaskInstanceDTO == null)
            {
                return BadRequest("TaskGroupInstanceDTO is null.");
            }

            var result = await _taskInstanceServices.AddTaskInstance(addtaskInstanceDTO);

            return CreatedAtAction(nameof(AddTaskInstance), new { id = result.ID }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskInstance(int id, [FromBody] UpdateTaskInstanceDTO updateTaskInstanceDTO)
        {
            // Call the service to update the TaskGroupInstance
            var updatedTaskInstance = await _taskInstanceServices.UpdateTaskInstance(id, updateTaskInstanceDTO);

            // Check if the update was successful
            if (updateTaskInstanceDTO == null)
            {
                return NotFound(); // Return NotFound if the entity to be updated is not found
            }

            return CreatedAtAction(nameof(UpdateTaskInstance), new { id = updatedTaskInstance.ID }, updatedTaskInstance);
        }

        [HttpDelete]
        [Route("DeleteTaskInstance/{id:int}")]
        public async Task<IActionResult> DeleteTaskInstance(int id)
        {
            var result = await _taskInstanceServices.DeleteTaskInstance(id);

            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
