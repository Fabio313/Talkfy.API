using AutoMapper;
using Domain.Commands.v1.Users.CreateUser;
using Domain.Entities.v1;

namespace Domain.Queries.v1.Users.GetUserById
{
    public class GetUserByIdQueryProfile : Profile
    {
        public GetUserByIdQueryProfile()
        {
            CreateMap<User, GetUserByIdQueryResponse>()
                .ForMember(dest => dest.Id, src => src.MapFrom(opt => opt.Id))
                .ForMember(dest => dest.Username, src => src.MapFrom(opt => opt.Username))
                .ForMember(dest => dest.Email, src => src.MapFrom(opt => opt.Email))
                .ForMember(dest => dest.CreatedDate, src => src.MapFrom(opt => opt.CreatedDate));
        }
    }
}
