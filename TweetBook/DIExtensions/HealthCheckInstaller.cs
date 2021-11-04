using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TweetBook.Data;
using TweetBook.HealthChecks;

namespace TweetBook.DIExtensions
{
    public static class HealthCheckInstaller
    {
        public static void InstallHealthCheckDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
            .AddDbContextCheck<DataContext>()
            .AddCheck<RedisHealthCheck>("Redis");
        }
    }

}
