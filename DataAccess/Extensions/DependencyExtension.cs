using DataAccess.Repositories;
using Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions
{
    public static class DependencyExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProjectsRepository, ProjectsRepository>();
        }
    }
}
