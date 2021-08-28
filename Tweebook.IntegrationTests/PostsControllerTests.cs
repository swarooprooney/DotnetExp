using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TweetBook.Contracts.V1;
using TweetBook.Domain;

namespace Tweetbook.IntegrationTests
{
    [TestClass]
    public class PostsControllerTests :IntegrationTests
    {
        [TestMethod]
        public async Task GetAll_WithoutAnyPosts_Return_Empty_Response()
        {
            //arrange
            await AuthenticateAsync();

            //act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetPosts);

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Post>>()).Should().BeEmpty();
        }
    }
}
