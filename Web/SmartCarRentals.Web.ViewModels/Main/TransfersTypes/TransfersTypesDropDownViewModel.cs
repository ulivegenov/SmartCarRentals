namespace SmartCarRentals.Web.ViewModels.Main.TransfersTypes
{
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.TransfersTypes;

    public class TransfersTypesDropDownViewModel : IMapFrom<TransfersTypesServiceDropDownModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
