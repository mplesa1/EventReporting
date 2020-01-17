using AutoMapper;
using EventReporting.Model;
using EventReporting.Model.Extensions;
using EventReporting.Model.User;
using EventReporting.Shared.DataTransferObjects.City;
using EventReporting.Shared.DataTransferObjects.Event;
using EventReporting.Shared.DataTransferObjects.Settlement;
using EventReporting.Shared.DataTransferObjects.User;

namespace EventReporting.BusinessLayer.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<CreateCityDto, City>();

            CreateMap<Settlement, SettlementDto>()
                .ForMember(src => src.TypeOfSettlement,
                           opt => opt.MapFrom(src => src.TypeOfSettlement.ToDescriptionString()));
            CreateMap<CreateSettlementDto, Settlement>()
                .ForMember(src => src.TypeOfSettlement, opt => opt.MapFrom(src => (ETypeOfSettlement)src.TypeOfSettlement));

            CreateMap<Event, EventDto>();
            CreateMap<CreateEventDto, Event>();

            CreateMap<User, UserDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<RegistrationDto, User>();
        }
    }
}
