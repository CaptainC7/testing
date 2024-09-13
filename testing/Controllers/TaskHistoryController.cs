using ClassLibraryDLL.Models.DTOs;
using ClassLibraryDLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace testing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskHistoryController : ControllerBase
    {
        private readonly ITaskHistoryServices _taskHistoryServices;

        public TaskHistoryController(ITaskHistoryServices taskHistoryServices)
        {
            _taskHistoryServices = taskHistoryServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskHistoryDTO>>> GetAllTaskHistoriesAsync()
        {
            var taskHistories = await _taskHistoryServices.GetAllTaskHistoriesAsync();
            return Ok(taskHistories);
        }
    }
}
