namespace SmartCarRentals.Services.Data.AppServices
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Data.Models.Enums.Reservation;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.AppServices.Contracts;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Cars;
    using SmartCarRentals.Services.Models.Administration.Drivers;

    public class HangfireService : IHangfireService
    {
        private readonly ICarsService carsService;
        private readonly IReservationsService reservationsService;
        private readonly IDriversService driversService;
        private readonly IDeletableEntityRepository<Car> carsRepository;
        private readonly IDeletableEntityRepository<Reservation> reservationsRepository;
        private readonly IDeletableEntityRepository<Transfer> transfersRepository;
        private readonly IDeletableEntityRepository<Driver> driversfersRepository;

        public HangfireService(
                               ICarsService carsService,
                               IReservationsService reservationsService,
                               IDriversService driversService,
                               IDeletableEntityRepository<Car> carsRepository,
                               IDeletableEntityRepository<Reservation> reservationsRepository,
                               IDeletableEntityRepository<Transfer> transfersRepository,
                               IDeletableEntityRepository<Driver> driversfersRepository)
        {
            this.carsService = carsService;
            this.reservationsService = reservationsService;
            this.driversService = driversService;
            this.carsRepository = carsRepository;
            this.reservationsRepository = reservationsRepository;
            this.transfersRepository = transfersRepository;
            this.driversfersRepository = driversfersRepository;
        }

        public async Task<int> CancelExpiredReservationsAsync()
        {
            var reservations = await this.reservationsService.GetAllAwaitingReservationsAsync();

            foreach (var reservation in reservations)
            {
                if (reservation.ReservationDate.CompareTo(DateTime.UtcNow) < 0 && reservation.Status != Status.Accomplished)
                {
                    reservation.Status = Status.Canceled;
                    this.reservationsRepository.Update(reservation.To<Reservation>());

                    var currentCar = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(reservation.CarId);
                    currentCar.HireStatus = HireStatus.Available;
                    currentCar.ReservationStatus = ReservationStatus.Free;
                    this.carsRepository.Update(currentCar.To<Car>());
                }
            }

            await this.reservationsRepository.SaveChangesAsync();
            var result = await this.carsRepository.SaveChangesAsync();

            return result;
        }

        public async Task<int> SettingUPTransfersStatusByDate()
        {
            var transfers = await this.transfersRepository.All()
                                                          .Where(t => t.TransferDate.Date.CompareTo(DateTime.UtcNow.Date) == 0
                                                                 && t.Status != SmartCarRentals.Data.Models.Enums.Transfer.Status.Finished)
                                                          .ToListAsync();

            foreach (var transfer in transfers)
            {
                transfer.Status = SmartCarRentals.Data.Models.Enums.Transfer.Status.OnGoing;

                this.transfersRepository.Update(transfer);
            }

            var result = await this.transfersRepository.SaveChangesAsync();

            return result;
        }
    }
}
