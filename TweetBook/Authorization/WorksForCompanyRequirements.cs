using Microsoft.AspNetCore.Authorization;

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
