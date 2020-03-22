namespace SmartCarRentals.Web.ViewModels.Administration.Parkings
{
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Parkings;

    public class ParkingsDropDownViewModel : IMapFrom<ParkingsServiceDropDawnModel>, IMapTo<ParkingsServiceDropDawnModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
