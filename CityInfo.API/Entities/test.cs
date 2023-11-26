using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("PointOfIntrestId")]
        public PointOfIntrest? PointOfIntrest { get; set; }
        public int PointOfIntrestId {  get; set; }
    }
}
