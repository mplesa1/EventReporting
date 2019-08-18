using AutoMapper;
using EventReporting.Model;
using EventReporting.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReporting.BusinessLayer.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<CreateCityDto, City>();
        }
    }
}
