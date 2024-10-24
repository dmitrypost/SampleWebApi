using Autofac;
using SampleWebApi.Infrastructure.Configuration;
using SampleWebApi.Infrastructure.Helpers;

namespace SampleWebApi.Autofac
{
    public class AuthModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var jwtSecret = EnvironmentVariableReader.GetValue(EnvironmentVariableKeys.JwtSecret);
            var tokenExpiresInDays = int.Parse(EnvironmentVariableReader.GetValue(EnvironmentVariableKeys.TokenExpiresInDays));
            
            builder.Register(context => new TokenGenerator(jwtSecret, tokenExpiresInDays))
                .As<ITokenGenerator>()
                    .InstancePerLifetimeScope();

            builder.RegisterType<PasswordHashing>()
                .As<IHashPasswords>()
                .InstancePerLifetimeScope();
        }
    }
}
