using AutoMapper;

namespace Dimogir.Web
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