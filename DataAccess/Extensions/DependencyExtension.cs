using DataAccess.Repositories;
using Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Abstractions;

namespace DataAccess.Extensions
{
    public static class DependencyExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProjectsRepository, ProjectsRepository>();

            services.AddTransient<ITasksRepository, TasksRepository>();

            services.AddTransient<IDesksRepository, DesksRepository>();

            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IRolesRepository, RolesRepository>();
        }
    }
}
