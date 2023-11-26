using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class PointOfIntrestProfile:Profile
    {
        public PointOfIntrestProfile()
        {
            CreateMap<Entities.PointOfIntrest, Models.PointOfIntrestsDto>();

            CreateMap<Models.PointOfIntrestsForCreationDto, Entities.PointOfIntrest>();

            CreateMap<Models.PointOfIntrestsForUpdateDto, Entities.PointOfIntrest>();
            CreateMap<Entities.PointOfIntrest, Models.PointOfIntrestsForUpdateDto > ();
            CreateMap<Entities.test, Models.TestDto>();
        }
    }
}
