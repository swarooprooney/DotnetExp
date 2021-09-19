using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Contracts.V1.Response
{
    public class PostResponse
    {
        public Guid PostId { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
