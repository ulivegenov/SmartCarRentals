namespace SmartCarRentals.Web.ViewModels.Administration.TransfersTypes
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.TransfersTypes;

    public class TransferTypeDetailsViewModel : IMapFrom<TransferTypeServiceDetailsModel>, IMapTo<TransferTypeServiceDetailsModel>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
    }
}
