using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointOfIntrests")]
    [ApiController]
    
    public class PointOfIntrestsController : ControllerBase
    {
        private readonly CitiesDataStore _citiesDataStore;
        private readonly IMailService _MailServce;
        private readonly ILogger<PointOfIntrestsController> _logger;
        private readonly IMapper _mapper;
        private readonly ICityInfoRepositry _cityInfoRepositry;

        public PointOfIntrestsController(ILogger<PointOfIntrestsController> logger,
            IMailService MailService, 
            IMapper mapper,
            ICityInfoRepositry cityInfoRepositry)
        {

            //_citiesDataStore = dataStore ??throw new ArgumentNullException(nameof(dataStore));  
            _mapper = mapper??throw new ArgumentNullException(nameof(mapper));
            _cityInfoRepositry = cityInfoRepositry?? throw new ArgumentException(nameof(cityInfoRepositry)); 
            _MailServce = MailService?? throw new ArgumentNullException(nameof(MailService)); 
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        }





        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfIntrestsDto>>> GetPointOfIntrests(int cityId) 
        {
            if (!await _cityInfoRepositry.CityExistsAysnc(cityId)) 
            {
                return NotFound();
            }
            
            var city =await _cityInfoRepositry.GetPointOfIntrestsForCityAsync(cityId);
           
            return Ok(_mapper.Map<IEnumerable<PointOfIntrestsDto>>(city));
           
            
            //try
            //{
                
            //    if (city == null)
            //    {
            //        _logger.LogInformation("sssssssss");
            //        return NotFound();

            //    }
            //    return Ok(city.pointOfIntrests);
            //}catch (Exception ex)
            //{
            //    _logger.LogCritical("Expetion while getting point of intrest ", ex);
            //    return StatusCode(500,"A problem happend when handeling this request ");

            //}
        }


        [HttpGet("{pointOfIntrestId}",Name = "GetPointOfIntrest")]
        public ActionResult<PointOfIntrestsDto> GetPointOfIntrest(int cityId ,int pointOfIntrestId) {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();

            }
            var pointOfIntrest=city.pointOfIntrests.FirstOrDefault(p => p.Id == pointOfIntrestId);
            if (pointOfIntrest == null)
            {
                return NotFound();
            }
            return Ok(pointOfIntrest);

        }

        [HttpPost]
        public async Task< ActionResult<PointOfIntrestsDto>> CreatePointOfIntrestDto(int cityId,
            [FromBody]PointOfIntrestsForCreationDto pointOfIntrest) {
            
           if (!await _cityInfoRepositry.CityExistsAysnc(cityId))
            {
                return NotFound();

            }
            var finalPointOfIntrest = _mapper.Map<PointOfIntrest>(pointOfIntrest);

            await _cityInfoRepositry.AddPointOfIntrestForCityAsync(cityId, finalPointOfIntrest);
            await _cityInfoRepositry.SaveChangesAsync();

            var createdPointOfIntrest = _mapper.Map<PointOfIntrestsDto>(finalPointOfIntrest);

            return CreatedAtRoute("GetPointOfIntrest",
                new
                {
                    cityId = cityId,
                    pointOfIntrestId=finalPointOfIntrest.Id

                },createdPointOfIntrest
                );;
        }



        [HttpPut("{pointOfIntrestId}")]
        public async Task<ActionResult> UpdatePointOfIntrest(int cityId, int pointOfIntrestId,
            [FromBody]PointOfIntrestsForUpdateDto pointOfIntrest)
        {
            if (!await _cityInfoRepositry.CityExistsAysnc(cityId))
            {
                return NotFound();   
            }
            var pointOfIntrestEntity = await _cityInfoRepositry.GetPointOfIntrestForCityAsync(cityId,pointOfIntrestId);

           
            if (pointOfIntrestEntity == null)
            {
                return NotFound();
            }
             _mapper.Map(pointOfIntrest, pointOfIntrestEntity);
            
            await _cityInfoRepositry.SaveChangesAsync( );
            return NoContent();
            
            
            
        
        }

        [HttpPatch("{pointOfIntrestId}")]
        public async Task<ActionResult> PartilyUpdatePointOfIntrest(int cityId, int pointOfIntrestId, 
            JsonPatchDocument<PointOfIntrestsForUpdateDto> patchDocument) 
        {

           
            if (!await _cityInfoRepositry.CityExistsAysnc(cityId))
            {
                return NotFound(); 
            }

            var pointOfIntrestEntity = await _cityInfoRepositry.GetPointOfIntrestForCityAsync (cityId, pointOfIntrestId);
            if (pointOfIntrestEntity == null) { 
                return NotFound(); 
            }

            var pointOfIntrestToPatch = _mapper.Map<PointOfIntrestsForUpdateDto>(pointOfIntrestEntity);
            
            patchDocument.ApplyTo(pointOfIntrestToPatch, ModelState);//model state here because see if the arguemt match here we need to check it the conttroler anotion above not check it 

            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfIntrestToPatch))//here he will validate like if not empty according to antiotion we put in the model like required
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(pointOfIntrestToPatch, pointOfIntrestEntity);
            await _cityInfoRepositry.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{pointOfIntrestId}")]
        public ActionResult DeletePointOfIntrest(int cityId, int pointOfIntrestId) 
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null) { return NotFound(); }

            var pointOfIntresstFromStore = city.pointOfIntrests.FirstOrDefault(p => p.Id == pointOfIntrestId);
            if (pointOfIntresstFromStore == null) { return NotFound(); }


            city.pointOfIntrests.Remove(pointOfIntresstFromStore);
            _MailServce.Send("point Of Intrest", $"the point with id {pointOfIntrestId} is deleted ");
            return NoContent();
        }

    }
}
