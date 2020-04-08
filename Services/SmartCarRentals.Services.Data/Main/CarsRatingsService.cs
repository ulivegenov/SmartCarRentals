namespace SmartCarRentals.Services.Data.Main
{
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Main.Contracts;

    public class CarsRatingsService : BaseService<CarRating, int>, ICarsRatingsService
    {
        public CarsRatingsService(IDeletableEntityRepository<CarRating> carsRatingsRepository)
            : base(carsRatingsRepository)
        {
        }
    }
}
