using Refit;
using System.Threading.Tasks;
using Tweetbook.Contracts.V1.Request.Queries;
using Tweetbook.Sdk;

namespace Tweetbook.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cachedToken = string.Empty;
            var identityApi = RestService.For<IIndentityApi>("https://localhost:44348");
            
            var registerResponse = await identityApi.RegisterAsync(new TweetBook.Contracts.V1.Request.UserRegistrationRequest
            {
                Email = "testsdk@test.com",
                Password = "Test1234!"
            });

            var loginResponse = await identityApi.LoginAsync(new TweetBook.Contracts.V1.Request.UserLoginRequest
            {
                UserName = "testsdk@test.com",
                Password = "Test1234!"
            });

            cachedToken = loginResponse.Content.Token;
            var tweetbookApi = RestService.For<ITweetbookApi>("https://localhost:44348", new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(cachedToken)
            });
            var paginationQuery = new PaginationQuery
            {
                PageNumber = 1,
                PageSize = 2
            };
            var allPosts = await tweetbookApi.GetPostsAsync(paginationQuery);

            var createPost = await tweetbookApi.CreateAsync(new TweetBook.Contracts.V1.Request.CreatePostRequest
            {
                Name = "Post from SDK"
            });
             allPosts = await tweetbookApi.GetPostsAsync(paginationQuery);
        }
    }
}
