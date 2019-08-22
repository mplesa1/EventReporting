﻿using AutoMapper;
using EventReporting.Model;
using EventReporting.Model.Extensions;
using EventReporting.Shared.DataTransferObjects.City;
using EventReporting.Shared.DataTransferObjects.Settlement;

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

        }
    }
}
