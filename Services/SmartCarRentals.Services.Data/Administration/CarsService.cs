namespace SmartCarRentals.Services.Data.Administration
{
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;

    public class CarsService : AdministrationService<Car, string>, ICarsService
    {
        private readonly IDeletableEntityRepository<Car> carRepository;

        public CarsService(IDeletableEntityRepository<Car> carRepository)
            : base(carRepository)
        {
            this.carRepository = carRepository;
        }
    }
}
