
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts.V1.Request.Queries;
using static TweetBook.Contracts.V1.ApiRoutes;

namespace TweetBook.Services
{
    public interface IUriService
    {
        Uri GetPostUri(string postId);

        Uri GetAllPostUri(PaginationQuery pagination = null);
    }

    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetAllPostUri(PaginationQuery pagination = null)
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

        public Uri GetPostUri(string postId)
        {
            return new Uri(_baseUri + "/" + Posts.Get.Replace("{postId}", postId));
        }
    }
}
