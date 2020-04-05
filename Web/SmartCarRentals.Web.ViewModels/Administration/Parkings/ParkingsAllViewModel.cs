namespace SmartCarRentals.Web.ViewModels.Administration.Parkings
{
    using System.Collections.Generic;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Parkings;

    public class ParkingsAllViewModel : IMapFrom<ParkingsServiceAllModel>
    {
        public ParkingsAllViewModel()
        {
            this.Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Town Town { get; set; }

        public string Address { get; set; }

        public int Capacity { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
