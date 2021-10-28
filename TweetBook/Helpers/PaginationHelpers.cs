using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts.V1.Request.Queries;
using Tweetbook.Contracts.V1.Response;
using TweetBook.Contracts.V1.Response;
using TweetBook.Domain;
using TweetBook.Services;

namespace TweetBook.Helpers
{
    public class PaginationHelpers
    {
        internal static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService, PaginationFilter paginationFilter, IEnumerable<T> response)
        {
            var nextPage = paginationFilter.PageNumber >= 1
            ? uriService.GetAllPostUri(new PaginationQuery(paginationFilter.PageNumber + 1, paginationFilter.PageSize)).ToString()
            : null;
            var previousPage = paginationFilter.PageNumber - 1 >= 1
                ? uriService.GetAllPostUri(new PaginationQuery(paginationFilter.PageNumber + 1, paginationFilter.PageSize)).ToString()
                : null;
            return new PagedResponse<T>
            {
                Data = response,
                PageNumber = paginationFilter.PageNumber >= 1 ? paginationFilter.PageNumber : null,
                PageSize = paginationFilter.PageSize >= 1 ? paginationFilter.PageSize : null,
                NextPage = response.Any() ? nextPage : null,
                PreviousPage = previousPage
            };

        }
    }
}
