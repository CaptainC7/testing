using ClassLibraryDLL.Models.DTOs;
using ClassLibraryDLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace testing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonHistoryController : ControllerBase
    {
        private readonly IPersonHistoryServices _personHistoryService;

        public PersonHistoryController(IPersonHistoryServices personHistoryService)
        {
            _personHistoryService = personHistoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonHistoryDTO>>> GetAllPersonHistoriesAsync()
        {
            var personHistories = await _personHistoryService.GetAllPersonHistoriesAsync();
            return Ok(personHistories);
        }
    }
}
