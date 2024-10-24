using MediatR;
using SampleWebApi.Infrastructure.Command.Requests;
using SampleWebApi.Infrastructure.Data;
using SampleWebApi.Infrastructure.Dtos.Results;
using SampleWebApi.Infrastructure.Helpers;

namespace SampleWebApi.Infrastructure.Command.Handlers;

public class CreateUserHandler(IUnitOfWork unitOfWork, IHashPasswords hashPasswords) : IRequestHandler<CreateUserRequest, UserResult>
{
    public async Task<UserResult> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var hashedPassword = hashPasswords.HashPassword(request.Password);
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth,     
            PasswordHash = hashedPassword
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