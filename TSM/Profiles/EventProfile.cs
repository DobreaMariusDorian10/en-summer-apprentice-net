using AutoMapper;
using TSM.Models;
using TSM.Models.Dto;

namespace TSM.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(v => v.Venue.LocationName))
                .ForMember(dest => dest.EventType, opt => opt.MapFrom(v => v.EventType.EventTypeName))
                .ReverseMap();

            CreateMap<Event, EventPatchDto>().ReverseMap();
        }
    }
   
}


