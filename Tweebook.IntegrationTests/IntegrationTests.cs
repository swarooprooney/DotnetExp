using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TweetBook;
using TweetBook.Contracts.V1;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;
using TweetBook.Data;

namespace Tweetbook.IntegrationTests
{
    public class IntegrationTests 
    {
        protected readonly HttpClient TestClient;
        protected IntegrationTests()
        {
            var dbName = Guid.NewGuid().ToString();
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(d =>
                            d.ServiceType == typeof(DbContextOptions<DataContext>));
                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }
                        services.AddDbContext<DataContext>(options =>
                        {
                            options.UseInMemoryDatabase(dbName);
                        });
                    });
                });
            TestClient = appFactory.CreateClient();
        }


        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<CreatePostResponse> CreatePostAsync(CreatePostRequest request)
        {
             var response = await TestClient.PostAsJsonAsync(ApiRoutes.Posts.CreatePost, request);
            return await response.Content.ReadAsAsync<CreatePostResponse>();
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Identity.Register, new UserRegistrationRequest
            {
                Email = "Test@integration.com",
                Password = "SomePass1234!"
            });
            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;

        }
    }
}
