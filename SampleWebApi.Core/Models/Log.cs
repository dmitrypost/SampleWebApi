namespace SampleWebApi.Core.Models
{
    public class Log: DbEntity
    {
        public string LogLevel { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
