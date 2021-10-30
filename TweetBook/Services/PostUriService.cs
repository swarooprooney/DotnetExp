using Microsoft.AspNetCore.WebUtilities;
using System;
using Tweetbook.Contracts.V1.Request.Queries;
using static TweetBook.Contracts.V1.ApiRoutes;

namespace TweetBook.Services
{
    public class PostUriService : IPostUriService
    {
        private readonly string _baseUri;
        public PostUriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetAllEntitiesUri(PaginationQuery pagination = null)
        {
            var uri = new Uri(_baseUri);
            if (pagination is null)
            {
                return uri;
            }

            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", pagination.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());

            return new Uri(modifiedUri);
        }

        public Uri GetEntityUri(string postId)
        {
            return new Uri(_baseUri + "/" + Posts.Get.Replace("{postId}", postId));
        }
    }
}
