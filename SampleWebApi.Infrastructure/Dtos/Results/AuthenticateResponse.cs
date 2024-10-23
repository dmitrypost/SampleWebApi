namespace SampleWebApi.Infrastructure.Dtos.Results
{
    public class AuthenticateResponse : BaseResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
