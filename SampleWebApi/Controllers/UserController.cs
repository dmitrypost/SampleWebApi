using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Extensions;
using SampleWebApi.Infrastructure.Command.Requests;

namespace SampleWebApi.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var result = await mediator.Send(new GetUserByIdRequest
                {
                    Id = id
                });

                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                return this.GetErrorResponse(ex);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var result = await mediator.Send(request);
            return this.GetResponse(result);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateUserById(UpdateUserRequest request)
        {
            var result = await mediator.Send(request);
            return this.GetResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            var result = await mediator.Send(new DeleteUserRequest
            {
                Id = id
            });

            return this.GetResponse(result);
        }
    }
}