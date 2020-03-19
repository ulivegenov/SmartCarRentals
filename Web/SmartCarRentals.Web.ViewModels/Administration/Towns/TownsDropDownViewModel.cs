namespace SmartCarRentals.Web.ViewModels.Administration.Towns
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class TownsDropDownViewModel : IMapFrom<Town>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
