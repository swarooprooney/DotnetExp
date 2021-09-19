using System;
using System.ComponentModel.DataAnnotations;

namespace TweetBook.Domain
{
    public class Tag
    {
        [Key]
        public Guid TagId { get; set; }

        public string TagName { get; set; }
    }
}
