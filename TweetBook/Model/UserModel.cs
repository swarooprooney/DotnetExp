﻿namespace TweetBook.Model
{
    public class UserModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get => Email; }
    }
}
