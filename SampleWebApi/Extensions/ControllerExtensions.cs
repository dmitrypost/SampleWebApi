using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Infrastructure.Dtos;
using System.Net;

namespace SampleWebApi.Extensions
{
    public static class ControllerBaseExtensions
    {

        public static IActionResult GetResponse(this ControllerBase controllerBase, BaseResponse response)
        {
            var httpResponse = new ObjectResult(response);
            httpResponse.StatusCode = response.ResponseCode;
            return httpResponse;
        }

        public static IActionResult GetErrorResponse(this ControllerBase controllerBase, Exception ex)
        {
            return controllerBase.GetResponse(new BaseResponse()
            {
                Message = ex.Message,
                Success = false,
                ResponseCode = (int)HttpStatusCode.InternalServerError
            });
        }
    }
}