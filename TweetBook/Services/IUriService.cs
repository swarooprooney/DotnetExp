using System;
using Tweetbook.Contracts.V1.Request.Queries;

namespace TweetBook.Services
{
    public interface IUriService
    {
        Uri GetEntityUri(string postId);

        Uri GetAllEntitiesUri(PaginationQuery pagination = null);
    }
}
