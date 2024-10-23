using MediatR;
using SampleWebApi.Infrastructure.Dtos.Results;

namespace SampleWebApi.Infrastructure.Command.Requests
{
    public class GetUserByIdRequest : IRequest<UserResult>
    {
        public int Id { get; set; }
    }
}
