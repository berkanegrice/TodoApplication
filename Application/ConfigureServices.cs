using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Todo.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}