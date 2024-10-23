using System.Net;
using MediatR;
using SampleWebApi.Infrastructure.Command.Requests;
using SampleWebApi.Infrastructure.Data;
using SampleWebApi.Infrastructure.Dtos.Results;

namespace SampleWebApi.Infrastructure.Command.Handlers
{
    public class GetUserHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserByIdRequest, UserResult>
    {
        public async Task<UserResult> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var dbUser = await unitOfWork.UserRepository.GetById(request.Id);

            if (dbUser == null)
            {
                return new UserResult
                {
                    Success = false,
                    Message = "User not found",
                    ResponseCode = (int)HttpStatusCode.NotFound
                };
            }

            return new UserResult
            {
                Id = dbUser.Id,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                Email = dbUser.Email,
                CreatedDate = dbUser.CreatedDate
            };
        }
    }
}
