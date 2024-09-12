using ClassLibraryDLL.Models.DTOs;
using ClassLibraryDLL.Services;
using ClassLibraryDLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace testing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskAttachmentController : ControllerBase
    {
        private readonly ITaskAttachmentServices _taskAttachmentServices;

        public TaskAttachmentController(ITaskAttachmentServices taskAttachmentServices)
        {
            _taskAttachmentServices = taskAttachmentServices;
        }

        [HttpGet("GetTaskAttachmentsByTaskInstanceID/{taskInstanceId}")]
        public async Task<ActionResult<IEnumerable<TaskAttachmentDTO>>> GetAllTaskAttachmentsByTaskInstanceId(int taskInstanceId)
        {
            try
            {
                var attachments = await _taskAttachmentServices.GetAllTaskAttachmentsByTaskInstanceId(taskInstanceId);

                if (attachments == null || !attachments.Any())
                {
                    return NotFound(new { message = "No attachments found for the specified TaskInstanceID." });
                }

                return Ok(attachments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskAttachmentDTO>> AddTaskAttachmentAsync(AddTaskAttachmentDTO addTaskAttachmentDTO)
        {
            if (addTaskAttachmentDTO == null)
            {
                return BadRequest("Invalid task attachment data.");
            }

            try
            {
                var createdAttachment = await _taskAttachmentServices.AddTaskAttachmentAsync(addTaskAttachmentDTO);

                if (createdAttachment == null)
                {
                    return StatusCode(500, "An error occurred while adding the task attachment.");
                }

                return CreatedAtAction(nameof(GetAllTaskAttachmentsByTaskInstanceId),
                                        new { taskInstanceId = createdAttachment.TaskInstanceID },
                                        createdAttachment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskAttachment(int id, [FromBody] AddTaskAttachmentDTO addTaskAttachmentDTO)
        {
            if (addTaskAttachmentDTO == null)
            {
                return BadRequest("UpdateTaskAttachmentDTO cannot be null.");
            }

            // Call the service to update the task attachment
            var updatedTaskAttachment = await _taskAttachmentServices.UpdateTaskAttachmentAsync(id, addTaskAttachmentDTO);

            if (updatedTaskAttachment == null)
            {
                return NotFound($"TaskAttachment with ID {id} not found.");
            }

            // Return the updated TaskAttachmentDTO
            return Ok(updatedTaskAttachment);
        }

        [HttpDelete]
        [Route("DeleteTaskAttachment/{id:int}")]
        public async Task<IActionResult> DeleteTaskAttachment(int id)
        {
            var result = await _taskAttachmentServices.DeleteTaskAttachment(id);

            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
