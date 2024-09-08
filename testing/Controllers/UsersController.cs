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
    public class UsersController : ControllerBase
    {
        private readonly IPersonServices _userServices;
        public UsersController(IPersonServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        [Route("/GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userServices.GetUsers();
            return Ok(users);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> AddPerson([FromBody] PersonDTO personDTO)
        {
            await _userServices.AddPerson(personDTO);
            return Ok(personDTO);
        }

        [HttpPut]
        [Route("UpdatePerson/{id:int}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonDTO personDTO)
        {
            await _userServices.UpdatePerson(id, personDTO);

            return Ok(personDTO);
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult CheckLogin(string username, string password)
        {
            var result = _userServices.CheckLogin(username, password);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetPerson/{id:int}")]
        public async Task<IActionResult> GetPersonByID (int id)
        {
            var person = await _userServices.GetPersonByID(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpDelete]
        [Route("DeletePerson/{id:int}")]
        public async Task<IActionResult> DeletPersonByID(int id)
        {
            var result = await _userServices.DeletePersonByID(id);

            if(!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
