using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController: ControllerBase
    {
      
        private readonly ICityInfoRepositry _cityInfoRepositry;
        private readonly IMapper _mapper;
        public CitiesController(ICityInfoRepositry  cityInfoRepositry,IMapper mapper) 
        {
            _mapper = mapper;
            _cityInfoRepositry = cityInfoRepositry?? throw new ArgumentNullException(nameof(cityInfoRepositry));
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<CityWithOutPointsOfIntrestDto>>> GetCities(){

            var cityEntities = await _cityInfoRepositry.GetCitiesAsync();
            var Reselt=new List<CityWithOutPointsOfIntrestDto>();
            
            return Ok(_mapper.Map<IEnumerable<CityWithOutPointsOfIntrestDto>>(cityEntities));

        }
        [HttpGet("{cityId}")]
        public async Task <IActionResult> GetCity(int cityId, bool includePointsOfIntrest=false ) {

            var city = await _cityInfoRepositry.GetCityAsync(cityId, includePointsOfIntrest);
            if (city == null)
            {
                return NotFound();
            }
            if (includePointsOfIntrest==false)
            {
                return Ok(_mapper.Map<CityWithOutPointsOfIntrestDto>(city));
            }
            return Ok(_mapper.Map<CityDto>(city));

            
           

            
        }
    }
}
