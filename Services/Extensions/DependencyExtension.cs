using Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;

namespace Services.Extensions;

public static class DependencyExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IUserEntityService, UserEntityService>();
    }
}