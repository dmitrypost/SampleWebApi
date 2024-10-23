using MediatR;
using SampleWebApi.Infrastructure.Command.Requests;
using SampleWebApi.Infrastructure.Data;
using SampleWebApi.Infrastructure.Dtos.Results;
using SampleWebApi.Infrastructure.Helpers;

namespace SampleWebApi.Infrastructure.Command.Handlers
{
    public class AuthenticateHandler(IUnitOfWork unitOfWork, IPasswordHashing passwordHashing, ITokenGenerator tokenGenerator) : IRequestHandler<AuthenticateRequest, AuthenticateResponse>
    {
        public async Task<AuthenticateResponse> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var user = unitOfWork.UserRepository.Get(user => user.Email == request.Email).SingleOrDefault();

                if (user == null)
                {
                    return new AuthenticateResponse
                    {
                        Success = false,
                        Message = "User not found"
                    };
                }

                var isValidPassword = passwordHashing.VerifyPassword(request.Password, user.PasswordHash);

                if (!isValidPassword)
                    return new AuthenticateResponse
                    {
                        Success = false,
                        Message = "Invalid password"
                    };

                var token = tokenGenerator.GenerateToken(user);

                return new AuthenticateResponse
                {
                    Email = user.Email,
                    Token = token,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
            }, cancellationToken);
        }
    }
}
