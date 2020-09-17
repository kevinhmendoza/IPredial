﻿using System.Collections.Generic;

namespace Core.UseCase.Contracts
{
    public interface IMapper
    {
        TDestination Map<TDestination>(object obj);
        TDestination Map<TSource, TDestination>(TSource obj, TDestination obj2);
        List<TDestination> Map<TSource, TDestination>(List<TSource> obj, List<TDestination> obj2);
        void CreateMapperConfiguration();
        void CreateMap<TSource, TDestination>();
        List<TDestination> Mappear<TSource, TDestination>(List<TSource> obj, List<TDestination> obj2);
        TDestination Mappear<TSource, TDestination>(TSource obj, TDestination obj2);
    }
}
