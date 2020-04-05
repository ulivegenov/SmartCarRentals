namespace SmartCarRentals.Web.ViewModels.Administration.TransfersTypes
{
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.TransfersTypes;

    public class TransferTypeEditInputModel : TransferTypeInputModel, IMapTo<TransferTypeServiceDetailsModel>
    {
        public int Id { get; set; }
    }
}
