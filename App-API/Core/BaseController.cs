using MediatR;

namespace Microsoft.AspNetCore.Mvc
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        
        private ISender _mediator = null!;
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();


        protected async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
        {
            return await Mediator.Send(query);
        }

        protected async Task<TResult> CommandAsync<TResult>(IRequest<TResult> command)
        {
            return await Mediator.Send(command);
        }

        protected ActionResult<T> Single<T>(T data)
        {
            if (data == null) return NotFound();
            return Ok(data);
        }
       
    }

    public static class BaseController 
    {
        public static decimal? GetEmployeeCode(this ControllerBase Controller)
        {
            var EmpCode  = Controller.User.Claims.Where(c => c.Type == "EmployeeCode").Select(x => x.Value).FirstOrDefault();


            if (EmpCode != null)
            {
                return Decimal.Parse(EmpCode);

            }

            return null;
        }
    }
   
}
