using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Dimogir.Web.App_Start
{
    public class AutomapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(a => a.AddProfile<MappingProfile>());

            //Test mappings
            Mapper.AssertConfigurationIsValid();
        }
    }
}