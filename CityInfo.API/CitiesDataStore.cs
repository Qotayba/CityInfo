using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
       // public static CitiesDataStore Current { get; }=new CitiesDataStore();
        public CitiesDataStore() {
            Cities = new List<CityDto>() {
             new CityDto() {
                Id = 1,
                Name = "Test",
                Description = "TestDescription",
                pointOfIntrests=new List <PointOfIntrestsDto>{
                    new PointOfIntrestsDto(){
                        Id = 1,
                        Name="pointOfIntrest1",
                        Description="ee"
                    }
                }
             },
             new CityDto() {
                Id = 2,
                Name = "Test2",
                Description = "TestDescription2",
                pointOfIntrests= new List <PointOfIntrestsDto>{ 
                    new PointOfIntrestsDto(){ 
                        Id = 1,
                        Name="pointOfIntrest1",
                        Description="ee"
                    },
                    new PointOfIntrestsDto(){
                        Id = 2,
                        Name="pointOfIntrest2",
                        Description="ee"
                    },
                    new PointOfIntrestsDto(){
                        Id = 3,
                        Name="pointOfIntrest3",
                        Description="ee"
                    }
                }
             },
            new CityDto()
            {
                Id = 3,
                Name = "Test2",
                Description = "TestDescription3",
                pointOfIntrests= new List <PointOfIntrestsDto>{
                    new PointOfIntrestsDto(){
                        Id = 1,
                        Name="pointOfIntrest1",
                        Description="ee"
                    },
                    new PointOfIntrestsDto(){
                        Id = 2,
                        Name="pointOfIntrest2",
                        Description="ee"
                    },
                    new PointOfIntrestsDto(){
                        Id = 3,
                        Name="pointOfIntrest3",
                        Description="ee"
                    }
                }
            },
        };
        }
    }
}
