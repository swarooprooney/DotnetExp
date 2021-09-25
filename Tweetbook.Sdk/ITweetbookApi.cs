using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;

namespace Tweetbook.Sdk
{
    [Headers("Authorization: Bearer")]
    public interface ITweetbookApi
    {
        [Get("/api/v1/posts")]
        Task<ApiResponse<IEnumerable<PostResponse>>> GetPostsAsync();

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
