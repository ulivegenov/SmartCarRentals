namespace SmartCarRentals.Services.Data.Administration
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Services.Data.Administration.Contracts;

    public class CarsService : AdministrationService<Car, string>, ICarsService
    {
        private const string InvalidCarIdErrorMessage = "{0} with ID: {1} does not exist.";
        private const string InvalidOperationErrorMessage = "Car with ID: {1} can't be deleted, because is currently in use.";

        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly IParkingsService parkingsService;

        public CarsService(
                           IDeletableEntityRepository<Car> carRepository,
                           IParkingsService parkingsService)
            : base(carRepository)
        {
            this.carRepository = carRepository;
            this.parkingsService = parkingsService;
        }

        public override async Task<int> DeleteByIdAsync(string id)
        {
            var car = await this.carRepository.All()
                                              .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                throw new ArgumentNullException(string.Format(InvalidCarIdErrorMessage, id));
            }

            if (car.ReservationStatus == ReservationStatus.Reserved || car.ParkingId == null)
            {
                throw new InvalidOperationException(string.Format(InvalidOperationErrorMessage, id));
            }

            var carParking = await this.parkingsService.GetByCarIdAsync(id);
            carParking.Cars.Remove(car);

            this.carRepository.Delete(car);
            var result = await this.carRepository.SaveChangesAsync();

            return result;
        }
    }
}
