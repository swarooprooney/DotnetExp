using Refit;
using System;
using System.Threading.Tasks;
using Tweetbook.Contracts.V1.Request.Queries;
using Tweetbook.Contracts.V1.Response;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;

namespace Tweetbook.Sdk
{
    [Headers("Authorization: Bearer")]
    public interface ITweetbookApi
    {
        [Get("/api/v1/posts")]
        Task<ApiResponse<PagedResponse<PostResponse>>> GetPostsAsync([Query] PaginationQuery paginationQuery);

        [Get("/api/v1/posts/{postId}")]
        Task<ApiResponse<PostResponse>> GetAsync(Guid postId);

        [Put("/api/v1/posts/{postId}")]
        Task<ApiResponse<PostResponse>> UpdateAsync(Guid postId, [Body] UpdatePost postToBeUpdated);

        [Post("/api/v1/posts")]
        Task<ApiResponse<CreatePostResponse>> CreateAsync([Body] CreatePostRequest createPost);

        [Delete("/api/v1/posts/{postId}")]
        Task<ApiResponse<string>> DeleteAsync(Guid postId);
    }
}
