using MediatR.Pipeline;
using SampleWebApi.Infrastructure.Command.Requests;

namespace SampleWebApi.Infrastructure.Command.PreProcessors;

public class CreateUserPreProcessor : IRequestPreProcessor<CreateUserRequest>
{
    public Task Process(CreateUserRequest request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}