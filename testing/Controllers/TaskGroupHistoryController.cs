using ClassLibraryDLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace testing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskGroupHistoryController : ControllerBase
    {
        private readonly ITaskGroupHistoryServices _taskGroupHistoryServices;

        public TaskGroupHistoryController(ITaskGroupHistoryServices taskGroupHistoryServices)
        {
            _taskGroupHistoryServices = taskGroupHistoryServices;
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetTaskGroupHistory()
        {
            var histories = await _taskGroupHistoryServices.GetTaskGroupHistory();
            return Ok(histories);
        }
    }
}
