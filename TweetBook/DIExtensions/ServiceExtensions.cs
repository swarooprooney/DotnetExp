using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using TweetBook.Filters;
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
            service.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Startup>());
            service.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });
        }
    }
}
