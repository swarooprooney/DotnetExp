using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Authorization
{
    public class WorksForCompanyRequirements : IAuthorizationRequirement
    {
        public string DomainName { get; }

        public WorksForCompanyRequirements(string domainName)
        {
            DomainName = domainName;
        }
    }
}
