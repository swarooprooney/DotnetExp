using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using TweetBook.Contracts.V1.Response;

namespace TweetBook.Examples.Response
{
    public class GetTagsExample : IExamplesProvider<IEnumerable<TagResponse>>
    {
        public IEnumerable<TagResponse> GetExamples()
        {
            return new List<TagResponse>
            {
                new TagResponse
                {
                    TagId = Guid.NewGuid(),
                    TagName = "Sample Tag1"
                },
                new TagResponse
                {
                    TagId = Guid.NewGuid(),
                    TagName = "Sample Tag2"
                }

            };
        }
    }
}
