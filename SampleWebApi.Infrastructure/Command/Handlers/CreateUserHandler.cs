using MediatR;
using SampleWebApi.Infrastructure.Command.Requests;
using SampleWebApi.Infrastructure.Data;
using SampleWebApi.Infrastructure.Dtos.Results;

namespace SampleWebApi.Infrastructure.Command.Handlers;

public class CreateUserHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserRequest, UserResult>
{
    public async Task<UserResult> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth,                
        };

        await unitOfWork.UserRepository.Insert(user);
        unitOfWork.Save();

        return new UserResult
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            CreatedDate = user.CreatedDate,
        };
    }
}