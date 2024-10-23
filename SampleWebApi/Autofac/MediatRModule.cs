using Autofac;
using MediatR;
using SampleWebApi.Shared.Pipelines;
using System.Reflection;
using MediatR.Pipeline;
using SampleWebApi.Infrastructure.Command.Handlers; // Required for Assembly
using Module = Autofac.Module;

namespace SampleWebApi.Autofac
{
    public class MediatRModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = new[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(IMediator).Assembly
            };

            // Register all MediatR request handlers
            builder.RegisterAssemblyTypes(assemblies)
                .AsImplementedInterfaces();

            // Register all MediatR handlers (IRequestHandler)
            builder.RegisterAssemblyTypes(typeof(GetUserHandler).Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Register all MediatR request pre-processors (IRequestPreProcessor)
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.IsClosedTypeOf(typeof(IRequestPreProcessor<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Register MediatR services
            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            // Register the LoggingPipeline as a pipeline behavior
            builder.RegisterGeneric(typeof(LoggingPipeline<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerLifetimeScope();
        }
    }
}