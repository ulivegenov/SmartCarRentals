namespace SmartCarRentals.Web.ViewModels.Administration.Parkings
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Parkings;

    public class ParkingsDropDownViewModel : IMapFrom<ParkingsServiceDropDownModel>, IMapTo<ParkingsServiceDropDownModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TownId { get; set; }

        public virtual Town Town { get; set; }
    }
}
