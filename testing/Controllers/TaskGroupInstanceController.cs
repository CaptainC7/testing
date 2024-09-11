using ClassLibraryDLL.Models.DTOs;
using ClassLibraryDLL.Services;
using ClassLibraryDLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace testing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskGroupInstanceController : ControllerBase
    {
        private readonly ITaskGroupInstanceServices _taskGroupInstanceServices;

        public TaskGroupInstanceController(ITaskGroupInstanceServices taskGroupInstanceController)
        {
            _taskGroupInstanceServices = taskGroupInstanceController;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskListInstanceDTO>>> GetAllTaskListInstances(int taskListInstanceId)
        {
            var result = await _taskGroupInstanceServices.GetTaskGroupInstancesByTaskListInstanceID(taskListInstanceId);
            return Ok(result);
        }


        [HttpPost]
        [Route("/CreateTaskGroupInstance")]
        public async Task<ActionResult<TaskGroupInstanceDTO>> AddTaskGroupInstance([FromBody] AddTaskGroupInstanceDTO addtaskInstanceGroupDTO)
        {
            if (addtaskInstanceGroupDTO == null)
            {
                return BadRequest("TaskGroupInstanceDTO is null.");
            }

            var result = await _taskGroupInstanceServices.AddTaskGroupInstance(addtaskInstanceGroupDTO);

            return CreatedAtAction(nameof(AddTaskGroupInstance), new { id = result.ID }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskGroupInstance(int id, [FromBody] AddTaskGroupInstanceDTO addTaskGroupInstanceDTO)
        {
            // Call the service to update the TaskGroupInstance
            var updatedTaskGroupInstance = await _taskGroupInstanceServices.UpdateTaskGroupInstance(id, addTaskGroupInstanceDTO);

            // Check if the update was successful
            if (addTaskGroupInstanceDTO == null)
            {
                return NotFound(); // Return NotFound if the entity to be updated is not found
            }

            return CreatedAtAction(nameof(UpdateTaskGroupInstance), new { id = updatedTaskGroupInstance.ID }, updatedTaskGroupInstance);
        }

        [HttpDelete]
        [Route("DeleteTaskGroupInstance/{id:int}")]
        public async Task<IActionResult> DeleteTaskGroupInstance(int id)
        {
            var result = await _taskGroupInstanceServices.DeleteTaskGroupInstance(id);

            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
