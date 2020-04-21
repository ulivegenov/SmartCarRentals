namespace SmartCarRentals.Services.Models.Main.Transfers
{
    using AutoMapper;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Transfer;
    using SmartCarRentals.Services.Mapping;

    public class TransferServicePointsModel : IMapFrom<Transfer>, IMapTo<Transfer>//, IHaveCustomMappings
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public int Points { get; set; }

        public int TransferTypeId { get; set; }

        public TransferType Type { get; set; }

        public bool HasPaid { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.fo
        //}
    }
}
