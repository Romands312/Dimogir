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
                .ForMember("Key", cfg => cfg.MapFrom(src => src.Id));

            CreateMap<Lesson, LessonViewModel>()
                .ForMember("Key", cfg => cfg.MapFrom(src => src.Id.ToString()));
        }
    }
}