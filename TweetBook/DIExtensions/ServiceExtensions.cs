using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        }
    }
}
