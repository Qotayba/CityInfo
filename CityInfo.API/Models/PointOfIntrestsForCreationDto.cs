using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfIntrestsForCreationDto
    {
        [Required(ErrorMessage ="the name is reqired")]//u can write required without message
        [MaxLength(50)]
        public string Name { get; set; }=string.Empty;
        [MaxLength(50)]
        public string? Description { get; set; }
    }
}
