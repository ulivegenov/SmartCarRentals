namespace SmartCarRentals.Services.Models.Main.TransfersTypes
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class TransfersTypesServiceDropDownModel : IMapFrom<TransferType>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
