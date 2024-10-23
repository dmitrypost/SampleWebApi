using MediatR;
using SampleWebApi.Shared.Logging;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Newtonsoft.Json;

namespace SampleWebApi.Shared.Pipelines
{
    public class LoggingPipeline<TRequest, TResponse>(ILogger logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = request.GetType().Name;
            var requestGuid = Guid.NewGuid().ToString();

            var requestNameWithGuid = $"{requestName} [{requestGuid}]";

            logger.LogInformation($"[START] {requestNameWithGuid}");
            TResponse response;

            var stopwatch = Stopwatch.StartNew();
            try
            {
                var requestJson = string.Empty;
                try
                {
                    //requestJson = JsonConvert.SerializeObject(request);
                    //logger.LogInformation($"[PROPS] {requestNameWithGuid} {requestJson}");
                }
                catch (NotSupportedException ex)
                {
                    logger.LogError($"[Serialization ERROR] {requestNameWithGuid} Could not serialize the request.", ex);
                }

                try
                {
                    response = await next();
                }
                catch (Exception ex)
                {
                    logger.LogError($"{requestNameWithGuid}; {requestJson}", ex);
                    throw;
                }
                
            }
            finally
            {
                stopwatch.Stop();
                logger.LogInformation(
                    $"[END] {requestNameWithGuid}; Execution time={stopwatch.ElapsedMilliseconds}ms");
            }

            return response;
        }
    }
}
