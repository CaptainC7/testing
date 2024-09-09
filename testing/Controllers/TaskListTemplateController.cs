using ClassLibraryDLL.Models;
using ClassLibraryDLL.Models.DTOs;
using ClassLibraryDLL.Services;
using ClassLibraryDLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
        [Route("/GetTemplates")]
        public async Task<IActionResult> GetTemplates()
        {
            var templates = await _taskListTemplateServices.GetTemplates();
            return Ok(templates);
        }

        [HttpPost]
        [Route("CreateTemplate")]
        public async Task<IActionResult> AddTemplate([FromBody] TaskListTemplateDTO taskListTemplateDTO)
        {
            await _taskListTemplateServices.AddTemplate(taskListTemplateDTO);
            return Ok(taskListTemplateDTO);
        }

        [HttpPut]
        [Route("UpdateTemplate/{id:int}")]
        public async Task<IActionResult> UpdateTemplate(int id, [FromBody] TaskListTemplateDTO taskListTemplateDTO)
        {
            await _taskListTemplateServices.UpdateTemplate(id, taskListTemplateDTO);

            return Ok(taskListTemplateDTO);
        }

        [HttpGet]
        [Route("GetTemplate/{id:int}")]
        public async Task<IActionResult> GetTemplateByID(int id)
        {
            var template = await _taskListTemplateServices.GetTemplateByID(id);

            if (template == null)
            {
                return NotFound();
            }

            return Ok(template);
        }

        [HttpDelete]
        [Route("DeleteTemplate/{id:int}")]
        public async Task<IActionResult> DeleteTemplateByID(int id)
        {
            var result = await _taskListTemplateServices.DeleteTemplateByID(id);

            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
