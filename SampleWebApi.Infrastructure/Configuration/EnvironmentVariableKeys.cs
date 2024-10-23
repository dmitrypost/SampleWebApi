namespace SampleWebApi.Infrastructure.Configuration
{
    public static class EnvironmentVariableKeys
    {
        // Database connection strings
        public const string DatabaseConnectionString = "SampleWebApi.ConnectionString";
        public const string JwtSecret = "SampleWebApi.JwtSecret";
        public const string TokenExpiresInDays = "SampleWebApi.TokenExpiresInDays";
    }
}