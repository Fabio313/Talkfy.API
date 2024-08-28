using AutoMapper;
using Domain.Entities.v1;

namespace Domain.Commands.v1.Messages.SendMessage
{
    public class SendMessageCommandProfile : Profile
    {
        public SendMessageCommandProfile() 
        {
            CreateMap<SendMessageCommand, Message>()
                .ForMember(dest => dest.Sender, src => src.MapFrom(opt => opt.Sender))
                .ForMember(dest => dest.Reciver, src => src.MapFrom(opt => opt.Reciver))
                .ForMember(dest => dest.Text, src => src.MapFrom(opt => opt.Text))
                .ForMember(dest => dest.SendDate, src => src.MapFrom(opt => opt.SendDate));
        }
    }
}
