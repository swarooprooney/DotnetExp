﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Options
{
    public class JwtOptions
    {
        public string Secret { get; set; }

        public TimeSpan TokenLifetime { get; set; }
    }
}
