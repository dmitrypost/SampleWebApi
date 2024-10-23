using Autofac;
using NLog;
using System;
using ILogger = SampleWebApi.Shared.Logging.ILogger;
using Logger = SampleWebApi.Shared.Logging.Logger;

namespace SampleWebApi.Autofac
{
    public class LoggerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var configFilePath = $"configuration/NLog.config";
            LogManager.Setup().LoadConfigurationFromFile(configFilePath);

            // Register the NLog logger as NLog.ILogger
            builder.Register(context => LogManager.GetCurrentClassLogger())
                .As<NLog.ILogger>()
                .SingleInstance(); // NLog's logger is typically a singleton

            // Register your custom logger (SampleWebApi.Shared.Logging.Logger) as ILogger
            builder.RegisterType<Logger>()
                .As<ILogger>()
                .InstancePerLifetimeScope(); // Adjust scope as needed
        }
    }
}