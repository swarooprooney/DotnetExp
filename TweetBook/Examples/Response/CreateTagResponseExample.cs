using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Response;

namespace TweetBook.Examples.Response
{
    public class CreateTagResponseExample : IExamplesProvider<CreateTagResponse>
    {
        public CreateTagResponse GetExamples()
        {
            return new CreateTagResponse { TagId = new Guid() };
        }
    }
}
