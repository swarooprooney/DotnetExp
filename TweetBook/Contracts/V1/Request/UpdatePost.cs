using System.ComponentModel.DataAnnotations;

namespace TweetBook.Contracts.V1.Request
{
    public class UpdatePost
    {
        [Required]
        public string Name { get; set; }
    }
}
