using CityInfo.API.Entities;

namespace CityInfo.API.Models
{
    public class PointOfIntrestsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TestDto? Test;
        
        
    }
}
