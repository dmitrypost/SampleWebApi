using MediatR.Pipeline;
using SampleWebApi.Infrastructure.Command.Requests;

namespace SampleWebApi.Infrastructure.Command.PreProcessors;

public class CreateUserPreProcessor : IRequestPreProcessor<CreateUserRequest>
{
    public Task Process(CreateUserRequest request, CancellationToken cancellationToken)
    {
        // rules here to check if the email is already registered...

        return Task.CompletedTask;
    }
}