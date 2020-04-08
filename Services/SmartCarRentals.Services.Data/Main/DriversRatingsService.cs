namespace SmartCarRentals.Services.Data.Administration
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.DraversRatings;

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

        public async Task<DriverRatingServiceDetailsModel> GetByTransferId(int transferId)
        {
            var driverRating = await this.driversRatingsEntityRepository.All()
                                                                        .FirstOrDefaultAsync(dr => dr.TransferId == transferId);

            return driverRating.To<DriverRatingServiceDetailsModel>();
        }

        public async Task<int> CreateAsync(DriverRatingServiceInputModel driverRatingServiceInputModel)
        {
            var driverRating = driverRatingServiceInputModel.To<DriverRating>();

            await this.driversRatingsEntityRepository.AddAsync(driverRating);
            var result = await this.driversRatingsEntityRepository.SaveChangesAsync();

            return result;
        }
    }
}
