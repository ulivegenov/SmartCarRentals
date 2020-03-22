namespace SmartCarRentals.Web.ViewModels.Administration.Towns
{
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Towns;

    public class TownsDropDownViewModel : IMapFrom<TownsServiceDropDownModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
