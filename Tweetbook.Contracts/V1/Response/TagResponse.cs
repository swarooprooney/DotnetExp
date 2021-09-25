using System;

namespace TweetBook.Contracts.V1.Response
{
    public class TagResponse
    {
        public Guid TagId { get; set; }

        public string TagName { get; set; }
    }
}
