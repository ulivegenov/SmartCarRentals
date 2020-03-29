namespace SmartCarRentals.Web.ViewModels.Administration.TransfersTypes
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.TransfersTypes;

    public class TransferTypeInputModel : IMapTo<TransferTypeServiceInputModel>, IMapFrom<TransferTypeServiceDetailsModel>
    {
        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
    }
}
