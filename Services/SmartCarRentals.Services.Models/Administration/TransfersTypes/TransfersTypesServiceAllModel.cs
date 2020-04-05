namespace SmartCarRentals.Services.Models.Administration.TransfersTypes
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class TransfersTypesServiceAllModel : IServiceAllModel, IMapFrom<TransferType>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
