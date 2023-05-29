using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Todo.Application.Common.Interfaces;
using Todo.Infrastructure.Persistence.Repositories;

namespace Todo.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ITaskItemRepository, TaskItemRepository>();
        return services;
    }
}