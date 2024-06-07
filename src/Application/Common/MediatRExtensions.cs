using Application.Behaviour;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Common
{
    public static class MediatRExtensions
    {
        public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryCachingPipelineBehaviour<,>));

            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(assembly);

            return services;
        }
    }
}
