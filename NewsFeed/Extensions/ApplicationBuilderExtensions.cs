using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace NewsFeed.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddDbContextFactory<DataContext>(this IServiceCollection services, string connectionString)
            where DataContext : DbContext
        {
            services.AddTransient<Func<DataContext>>((ctx) =>
            {
                var options = new DbContextOptionsBuilder<DataContext>()
                    .UseSqlServer(connectionString)
                    .Options;

                return () => (DataContext)Activator.CreateInstance(typeof(DataContext), options);
            });
        }

        public static void UseSqlTableDependency<T>(this IApplicationBuilder services, string connectionString)
            where T : IDatabaseSubscription
        {
            var serviceProvider = services.ApplicationServices;
            var subscription = serviceProvider.GetService<T>();
            subscription.Configure(connectionString);
        }
    }
}
