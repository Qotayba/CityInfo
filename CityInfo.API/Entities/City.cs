﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } =string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        public ICollection<PointOfIntrest> PointOfIntrests { get; set; } = new List<PointOfIntrest>();

        public City(string name) { 
            Name= name;
        }
    
                
    }   
}
