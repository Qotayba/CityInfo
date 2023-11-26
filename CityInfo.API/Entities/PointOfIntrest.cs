using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CityInfo.API.Entities
{
    public class PointOfIntrest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

        [ForeignKey("CityId")]
        public City? City { get; set; }

        public int CityId { get; set; }
        

        [ForeignKey("TestId")]
        public test? Test { get; set; }
        [AllowNull]
        public int? TestId { get; set; }

        public PointOfIntrest (string name)
        {
            Name = name;
        }


    }
}
