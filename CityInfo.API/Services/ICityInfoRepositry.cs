using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepositry
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<bool> CityExistsAysnc(int cityId);
        Task<City?> GetCityAsync(int cityId,bool includePointOfIntrest);

        Task<IEnumerable<PointOfIntrest>> GetPointOfIntrestsForCityAsync(int cityId);
        Task<PointOfIntrest?> GetPointOfIntrestForCityAsync(int cityId, int pointOfIntrestId);
        Task AddPointOfIntrestForCityAsync(int cityId, PointOfIntrest pointOfIntrest);
        Task<bool> SaveChangesAsync();
    }
}
