using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Contracts.V1.Response
{
    public class TagResponse
    {
        public Guid TagId { get; set; }

        public string TagName { get; set; }
    }
}
