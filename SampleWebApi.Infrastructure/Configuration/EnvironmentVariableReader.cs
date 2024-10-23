namespace SampleWebApi.Infrastructure.Configuration
{
    public static class EnvironmentVariableReader
    {
        public static string GetValue(string key)
        {
            // Retrieve a generic setting from the environment variables
            string value = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException($"Environment variable '{key}' is not set.");
            }
            return value;
        }
    }
}