using AutoMapper;
using TweetBook.Contracts.V1.Response;
using TweetBook.Domain;

namespace TweetBook.AutoMapperProfiles.V1
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Tag, TagResponse>();
            CreateMap<Post, PostResponse>().ForMember(dest=>dest.UserName,source=>source.MapFrom(x=>x.User.UserName));
        }
    }
}
