using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Domain
{
    public class User
    {
        private string userName;

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get => Email; }
    }
}
