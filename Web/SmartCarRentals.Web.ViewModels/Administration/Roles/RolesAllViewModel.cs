namespace SmartCarRentals.Web.ViewModels.Administration.Roles
{
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Roles;

    public class RolesAllViewModel : IMapFrom<RolesAllServiceModel>, IMapTo<RolesAllServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public bool IsSelected { get; set; }
    }
}
