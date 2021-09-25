using Swashbuckle.AspNetCore.Filters;
using TweetBook.Contracts.V1.Request;

namespace TweetBook.Examples.Request
{
    public class CreateTagRequestExample : IExamplesProvider<CreateTagRequest>
    {
        public CreateTagRequest GetExamples()
        {
            return new CreateTagRequest 
            { 
                TagName = "Sample tag name"
            };
        }
    }
}
