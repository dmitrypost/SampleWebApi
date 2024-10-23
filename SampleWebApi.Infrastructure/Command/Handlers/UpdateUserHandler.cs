using System.Net;
using MediatR;
using SampleWebApi.Infrastructure.Command.Requests;
using SampleWebApi.Infrastructure.Data;
using SampleWebApi.Infrastructure.Dtos.Results;

namespace SampleWebApi.Infrastructure.Command.Handlers
{
    public class UpdateUserHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserRequest, UserResult>
    {
        public async Task<UserResult> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
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

            dbUser.FirstName = request.FirstName;
            dbUser.LastName = request.LastName;
            dbUser.Email = request.Email;
            dbUser.DateOfBirth = request.DateOfBirth;

            await unitOfWork.UserRepository.Update(dbUser);
            unitOfWork.Save();

            return new UserResult
            {
                Id = dbUser.Id,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                Email = dbUser.Email,
                CreatedDate = dbUser.CreatedDate,
                ModifiedDate = dbUser.ModifiedDate
            };
        }
    }
}
