using Application.DTO.Helper;
using Application.Interfaces.HelperService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 
namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HelpersController : ControllerBase
    {
        private readonly IHelperService helperService;

        public HelpersController(IHelperService helperService)
        {
            this.helperService = helperService;
        }

        [Route("GetDropDowns")]
        [HttpPost]
        public async Task<ActionResult<DropDownsDTO>> GetDropDowns(List<int> PagesID)
        {
            var res= Ok(await helperService.FillDropDowns(PagesID));
            return res;
        }

        
 

    
        
    }
}
