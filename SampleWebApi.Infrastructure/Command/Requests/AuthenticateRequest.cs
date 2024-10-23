using MediatR;
using SampleWebApi.Infrastructure.Dtos.Results;

namespace SampleWebApi.Infrastructure.Command.Requests
{
    public class AuthenticateRequest : IRequest<AuthenticateResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}