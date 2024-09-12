using ClassLibraryDLL.Models;
using ClassLibraryDLL.Models.DTOs;
using ClassLibraryDLL.Services;
using ClassLibraryDLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace testing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListTemplateController : ControllerBase
    {
        private readonly ITaskListTemplateServices _taskListTemplateServices;
        public TaskListTemplateController(ITaskListTemplateServices taskListTemplateServices)
        {
            _taskListTemplateServices = taskListTemplateServices;
        }

        [HttpGet]
        [Route("GetAllTemplates")]
        public async Task<ActionResult<IEnumerable<TaskListTemplateDTO>>> GetAllTemplates()
        {
            var templates = await _taskListTemplateServices.GetAllTaskListTemplatesAsync();
            return Ok(templates);
        }

        [HttpPost]
        [Route("CreateTemplate")]
        public async Task<IActionResult> AddTemplateAsync([FromBody] AddTaskListTemplateDTO addTaskListTemplateDTO)
        {
            await _taskListTemplateServices.AddTemplateAsync(addTaskListTemplateDTO);
            return Ok(addTaskListTemplateDTO);
        }

        [HttpPut]
        [Route("UpdateTemplate/{id:int}")]
        public async Task<IActionResult> UpdateTemplate(int id, [FromBody] UpdateTaskListTemplateDTO updateTaskListTemplateDTO, int userID)
        {
            if (userID <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var updatedTemplate = await _taskListTemplateServices.UpdateTemplate(id, updateTaskListTemplateDTO, userID);
            if (updatedTemplate == null)
            {
                return NotFound();
            }

            return Ok(updatedTemplate);
        }

        [HttpGet]
        [Route("GetTemplate/{id:int}")]
        public async Task<IActionResult> GetTemplateByIDAsync(int id)
        {
            var template = await _taskListTemplateServices.GetTemplateByIDAsync(id);

            if (template == null)
            {
                return NotFound();
            }

            return Ok(template);
        }

        [HttpDelete]
        [Route("DeleteTemplate/{id:int}")]
        public async Task<IActionResult> DeleteTemplateByID(int id, int userID)
        {
            var result = await _taskListTemplateServices.DeleteTemplateByID(id, userID);

            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
