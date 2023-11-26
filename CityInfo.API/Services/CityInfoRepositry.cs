using AutoMapper.Configuration.Conventions;
using CityInfo.API.Controllers;
using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class CityInfoRepositry : ICityInfoRepositry
    {
        private readonly CityInfoContext _context;

        private readonly ILogger<CityInfoRepositry> _logger;
        public CityInfoRepositry(CityInfoContext context, ILogger<CityInfoRepositry> logger) {
            this._context = context ??throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
           var d= await _context.Cities.Include(c=>c.PointOfIntrests).ToListAsync();
            _logger.LogCritical($"{d}");
            return d;

        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointOfIntrest)
        {
            if (includePointOfIntrest) {
                return await _context.Cities
                    .Include(c => c.PointOfIntrests)
                       .ThenInclude(p=>p.Test)
                    .FirstOrDefaultAsync(c=> c.Id==cityId);
                    
            }
            return await _context.Cities.Where(c=> c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<PointOfIntrest?> GetPointOfIntrestForCityAsync(int cityId, int pointOfIntrestId)
        {
            return await _context.PointOfIntrest.Where(p=> p.CityId==cityId && p.Id==pointOfIntrestId).
                FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfIntrest>> GetPointOfIntrestsForCityAsync(int cityId)
        {
            return await _context.PointOfIntrest.Where(p=>p.CityId==cityId).ToListAsync();
        }

        public async Task<bool> CityExistsAysnc(int cityId)
        {
            return await _context.Cities.AnyAsync(c=>c.Id == cityId);
        }

        public async Task AddPointOfIntrestForCityAsync(int cityId, PointOfIntrest pointOfIntrest) {

            var city = await GetCityAsync(cityId,false);
            if (city != null) 
            { 
                city.PointOfIntrests.Add(pointOfIntrest);
            }


        }
        public async Task<bool> SaveChangesAsync() {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
