using ClassLibraryDLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace testing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskListTemplateHistoryController : ControllerBase
    {
        private readonly ITaskListTemplateHistoryServices _taskListTemplateHistoryServices;

        public TaskListTemplateHistoryController(ITaskListTemplateHistoryServices taskListTemplateHistoryServices)
        {
            _taskListTemplateHistoryServices = taskListTemplateHistoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHistoryRecords()
        {
            var histories = await _taskListTemplateHistoryServices.GetAllHistoryRecords();
            return Ok(histories);
        }
    }
}
