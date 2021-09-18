using AutoMapper;
using TweetBook.Contracts.V1.Response;
using TweetBook.Domain;

namespace TweetBook.AutoMapperProfiles.V1
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Tag, GetTagResponse>();
        }
    }
}
