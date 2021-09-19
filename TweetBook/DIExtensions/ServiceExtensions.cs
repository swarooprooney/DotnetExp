using Microsoft.Extensions.DependencyInjection;
using TweetBook.Services;

namespace TweetBook.DIExtensions
{
    public static class ServiceExtensions
    {
        public static void InstallServices(this IServiceCollection service)
        {
            service.AddScoped<IIdentityService, IdentityService>();
            service.AddScoped<IPostService, PostService>();
            service.AddScoped<ITagService, TagService>();
            service.AddAutoMapper(typeof(Startup));
        }
    }
}
