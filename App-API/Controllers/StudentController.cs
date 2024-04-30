using Application.DTO;
using Application.Handlers.Students.Commands;
using Application.Handlers.Students.Queries;
using Application.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ApiControllerBase
    {

        public StudentController() { }

        //Get Section
        [HttpPost]
        [Route("GetAll")]
        public async Task<ActionResult<List<StudentDTO>>> GetAllStudents(GetAllStudents getAll)
        {
            return Single(await QueryAsync(getAll));
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<ActionResult<StudentDTO>> GetById(GetStudentById getById)
        {
            return Single(await QueryAsync(getById));
        }

        //create
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Result>> CreateUser(CreateStudent createStudent)
        {
            return Single(await CommandAsync(createStudent));
        }



        [HttpPost]
        [Route("Delete")]
        public async Task<ActionResult<Result>> DeleteUser(DeleteStudent deleteStudent)
        {
            return Single(await CommandAsync(deleteStudent));
        }


        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult<Result>> UpdateUser(UpdateStudent updateStudent)
        {
            return Single(await CommandAsync(updateStudent));

        }
    }
}
