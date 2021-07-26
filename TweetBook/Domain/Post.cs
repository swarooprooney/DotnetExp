using System;
using System.ComponentModel.DataAnnotations;

namespace TweetBook.Domain
{
    public class Post
    {
        [Key]
        public Guid PostId { get; set; }

        public string Name { get; set; }
    }
}
