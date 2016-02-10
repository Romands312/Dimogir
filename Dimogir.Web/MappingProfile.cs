using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Dimogir.DomainModel;
using Dimogir.Web.ViewModels;

namespace Dimogir.Web
{
    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Category, CategoryViewModel>()
                .ForMember(dest => dest.Key, cfg => cfg.MapFrom(src => src.Id));

            CreateMap<Lesson, LessonViewModel>()
                .ForMember(dest => dest.Key, cfg => cfg.MapFrom(src => src.Id.ToString()));

            CreateMap<LessonEditViewModel, Lesson>()
                .ForMember(dest => dest.Id, cfg => cfg.Ignore());


        }
    }
}