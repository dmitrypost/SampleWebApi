using System.Net;
using MediatR;
using SampleWebApi.Infrastructure.Command.Requests;
using SampleWebApi.Infrastructure.Data;
using SampleWebApi.Infrastructure.Dtos.Results;

namespace SampleWebApi.Infrastructure.Command.Handlers
{
    public class DeleteUserHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserRequest, UserResult>
    {
        public async Task<UserResult> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository.GetById(request.Id);

            if (user == null)
            {
                return new UserResult
                {
                    Success = false,
                    Message = "User not found",
                    ResponseCode = (int)HttpStatusCode.NotFound
                };
            }

            await unitOfWork.UserRepository.Delete(request.Id);
            unitOfWork.Save();

            return new UserResult();
        }
    }
}
