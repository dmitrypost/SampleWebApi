using MediatR;
using SampleWebApi.Infrastructure.Dtos.Results;

namespace SampleWebApi.Infrastructure.Command.Requests
{
    public class CreateUserRequest : IRequest<UserResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}