using AutoMapper;
using EventReporting.Shared.Contracts.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReporting.BusinessLayer.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper _mapper;

        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Map object to desired type.
        /// </summary>
        /// <typeparam name="TSrouce"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        protected TDestination Map<TSrouce, TDestination>(TSrouce source)
        {
            return _mapper.Map<TSrouce, TDestination>(source);
        }


        /// <summary>
        /// Maps all properites to destination object.
        /// </summary>
        /// <typeparam name="TSrouce"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        protected TDestination MapToInstance<TSrouce, TDestination>(TSrouce source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}
