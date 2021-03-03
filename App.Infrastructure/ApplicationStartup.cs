using App.Domain.Feedbacks;
using App.Domain.IRepositories;
using App.Domain.Projects;
using App.Domain.SeedWork;
using App.Domain.Users;
using App.Domain.Users.Rules;
using App.Infrastructure.Database;
using App.Infrastructure.Processing;
using App.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure
{
    public static class ApplicationStartup
    {
        public static void InitializeServices(
            this IServiceCollection services, 
            string dbConnectionString,
            string migrationAssemblyPath)
        {

            services.AddScoped(typeof(IRepository<User>), typeof(Repository<User>));
            services.AddScoped(typeof(IRepository<Project>), typeof(Repository<Project>));
            services.AddScoped(typeof(IRepository<Feedback>), typeof(Repository<Feedback>));
            services.AddScoped(typeof(IDomainEventsDispatcher), typeof(DomainEventsDispatcher));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        
    
            services.AddDbContext<AppContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(dbConnectionString,
                    builder => builder.MigrationsAssembly(migrationAssemblyPath));
            });
        }
    }
}