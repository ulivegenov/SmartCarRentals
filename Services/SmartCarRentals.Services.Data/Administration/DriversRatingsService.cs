namespace SmartCarRentals.Services.Data.Administration
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;

    public class DriversRatingsService : IDriversRatingsService
    {
        private readonly IDeletableEntityRepository<DriverRating> driversRatingsEntityRepository;

        public DriversRatingsService(IDeletableEntityRepository<DriverRating> driversRatingsEntityRepository)
        {
            this.driversRatingsEntityRepository = driversRatingsEntityRepository;
        }

        public async Task<int> DeleteAllByDriverIdAsync(string driverId)
        {
            var driverRatings = await this.driversRatingsEntityRepository.All()
                                                                         .Where(dr => dr.DriverId == driverId)
                                                                         .ToListAsync();

            foreach (var driverRating in driverRatings)
            {
                this.driversRatingsEntityRepository.Delete(driverRating);
            }

            var result = await this.driversRatingsEntityRepository.SaveChangesAsync();

            return result;
        }
    }
}
