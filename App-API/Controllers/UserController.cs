using Application.DTO;
  using Application.Handlers.Users.Commends;
 using Application.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

     public class UserController : ApiControllerBase
    {

        public UserController()
        {
        }
        
        [HttpPost]
        [Route("GetbyId")]
        public async Task<ActionResult<UserDTO?>> GetUserById( GetUserById getUserById )
        {
            
            return Single(await QueryAsync(getUserById));
 
        }
        [HttpPost]
        [Route("GetAllUser")]
        public async Task<ActionResult<List<UserDTO>?>> GetAllUser(GetAllUser getUserById)
        {

            return Single(await QueryAsync(getUserById));

        }



        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Result>> CreateUser(CreateUser  createUser)
        {
            return Single(await CommandAsync(createUser));
        }

       

        [HttpPost]
        [Route("Delete")]
        public async Task<ActionResult<Result>> DeleteUser(DeleteUser deleteUser)
        {
             return Single(await CommandAsync(deleteUser));
        }
 
     
        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult<Result>> UpdateUser(UpdateUser updateUser)
        {
            return Single(await CommandAsync(updateUser));

        }

    }
}
