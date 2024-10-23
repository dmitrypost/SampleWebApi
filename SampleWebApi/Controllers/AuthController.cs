using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Extensions;
using SampleWebApi.Infrastructure.Command.Requests;

namespace SampleWebApi.Controllers
{
    public class AuthController(IMediator mediator) : Controller
    {
        public async Task<IActionResult> Authenticate(string email, string password)
        {
            try
            {
                var result = await mediator.Send(new AuthenticateRequest
                {
                    Email = email,
                    Password = password
                    // Add any additional properties here
                    // example: remember me to make the token last longer
                });

                return this.GetResponse(result);
            }
            catch (Exception ex)
            {
                return this.GetErrorResponse(ex);
            }
        }
    }
}
