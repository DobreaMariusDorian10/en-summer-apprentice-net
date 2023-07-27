using AutoMapper;
using TSM.Models;
using TSM.Models.Dto;

namespace TSM.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.EventName, opt => opt.MapFrom(v => v.TicketCategory.Event.EventName))
            //.ForMember(dest => dest.EventType, opt => opt.MapFrom(v => v.EventType.EventTypeName))
             .ReverseMap();

            CreateMap<Order, OrderPatchDto>().ReverseMap();
        }
    }

}
