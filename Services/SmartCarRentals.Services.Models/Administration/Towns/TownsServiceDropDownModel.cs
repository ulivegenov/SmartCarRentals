namespace SmartCarRentals.Services.Models.Administration.Towns
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class TownsServiceDropDownModel : IMapFrom<Town>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
