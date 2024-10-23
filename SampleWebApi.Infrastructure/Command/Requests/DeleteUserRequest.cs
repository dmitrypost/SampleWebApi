using MediatR;
using SampleWebApi.Infrastructure.Dtos;
using SampleWebApi.Infrastructure.Dtos.Results;

namespace SampleWebApi.Infrastructure.Command.Requests
{
    public class DeleteUserRequest : IRequest<UserResult>
    {
        public int Id { get; set; }
    }
}