using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Services;

namespace TweetBook.DIExtensions
{
    public static class PaginationExtensions
    {
        public static void AddPaginationSupportToApi(this IServiceCollection services)
        {
            services.AddScoped<IPostUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");
                return new PostUriService(absoluteUri);
            });
        }
    }
}
