﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Domain
{
    public class Tag
    {
        [Key]
        public Guid TagId { get; set; }

        public string TagName { get; set; }
    }
}
