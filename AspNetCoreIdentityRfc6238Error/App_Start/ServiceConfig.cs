using AspNetCoreIdentityRfc6238Error.Models;
using AspNetCoreIdentityRfc6238Error.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.WebApi;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace AspNetCoreIdentityRfc6238Error.App_Start
{
    public static class ServiceConfig
    {
        public static IContainer CreateContainer()
        {
            var services = new ServiceCollection();

            services.AddLogging();
            
            new IdentityBuilder(typeof(User), services)
                .AddUserManager<UserManager<User>>()
                .AddUserStore<UserStore>()
                .AddTokenProvider<AuthenticatorTokenProvider<User>>(TokenOptions.DefaultAuthenticatorProvider);

            services.TryAddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();

            var builder = new ContainerBuilder();

            builder.Populate(services);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            return container;
        }
    }
}