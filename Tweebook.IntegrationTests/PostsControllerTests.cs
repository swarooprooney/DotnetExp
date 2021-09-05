using FluentAssertions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TweetBook.Contracts.V1;
using TweetBook.Domain;
using Xunit;

namespace Tweetbook.IntegrationTests
{
    public class PostsControllerTests : IntegrationTests
    {
        [Fact]
        public async Task GetAll_WithoutAnyPosts_Returns_Empty_Response()
        {
            //arrange
            await AuthenticateAsync();

            //act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetPosts);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Post>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task Get_Returns_Posts_If_Posts_Exists()
        {
            //arrange
            await AuthenticateAsync();

            var createdPost = await CreatePostAsync(new TweetBook.Contracts.V1.Request.CreatePostRequest { Name = "Post from INT" });

            //act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.Get.Replace("{postId}", createdPost.PostId.ToString()));

            //assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var returnedPost = await response.Content.ReadAsAsync<Post>();
            returnedPost.Should().BeEquivalentTo(createdPost);
        }

    }
}
