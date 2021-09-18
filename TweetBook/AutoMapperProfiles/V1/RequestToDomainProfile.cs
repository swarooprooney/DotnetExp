using AutoMapper;
using TweetBook.Contracts.V1.Request;
using TweetBook.Domain;

namespace TweetBook.AutoMapperProfiles.V1
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateTagRequest, Tag>();
        }
    }
}
