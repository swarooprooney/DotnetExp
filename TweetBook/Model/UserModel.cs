﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Model
{
    public class UserModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get => Email; }
    }
}