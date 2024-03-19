using AspNetCoreIdentityRfc6238Error.Models;
using AspNetCoreIdentityRfc6238Error.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentityRfc6238Error.App_Start
{
    public static class ServiceConfig
    {
        public static IContainer CreateContainer()
        {
            var services = new ServiceCollection();

            new IdentityBuilder(typeof(User), services)
                .AddUserStore<UserStore>()
                .AddTokenProvider<AuthenticatorTokenProvider<User>>(TokenOptions.DefaultAuthenticatorProvider);

            var builder = new ContainerBuilder();
            builder.Populate(services);

            var container = builder.Build();

            return container;
        }
    }
}