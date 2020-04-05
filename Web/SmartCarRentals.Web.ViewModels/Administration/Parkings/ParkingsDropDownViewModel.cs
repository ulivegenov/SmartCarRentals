namespace SmartCarRentals.Web.ViewModels.Administration.Parkings
{
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Parkings;

    public class ParkingsDropDownViewModel : IMapFrom<ParkingsServiceDropDownModel>, IMapTo<ParkingsServiceDropDownModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
