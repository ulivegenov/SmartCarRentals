namespace SmartCarRentals.Services.Models.Roles
{
    using AutoMapper;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class RolesAllServiceModel : IMapFrom<ApplicationRole>, IMapTo<ApplicationRole>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public bool IsSelected { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, RolesAllServiceModel>()
                .ForMember(
                           destination => destination.UserId,
                           source => source.MapFrom(s => s.Id));

            configuration.CreateMap<RolesAllServiceModel, ApplicationUser>()
                .ForMember(
                           destination => destination.Id,
                           source => source.MapFrom(s => s.UserId));
        }
    }
}
