namespace SmartCarRentals.Web.ViewModels.Administration.TransfersTypes
{
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.TransfersTypes;

    public class TransfersTypesAllViewModel : IMapFrom<TransfersTypesServiceAllModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
