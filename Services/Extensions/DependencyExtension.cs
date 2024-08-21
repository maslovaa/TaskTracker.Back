using Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using Microsoft.Extensions;

namespace Services.Extensions;

public static class DependencyExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<INotificationAdapter, NotificationAdapter>();
        services.AddTransient<IUserEntityService, UserEntityService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddHttpClient();
    }
}