namespace SmartCarRentals.Services.Data.AppServices
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Reservation;
    using SmartCarRentals.Services.Data.AppServices.Contracts;
    using SmartCarRentals.Services.Data.Main.Contracts;

    public class HangfireService : IHangfireService
    {
        private readonly IReservationsService reservationsService;
        private readonly IDeletableEntityRepository<Transfer> transfersRepository;

        public HangfireService(
                               IReservationsService reservationsService,
                               IDeletableEntityRepository<Transfer> transfersRepository)
        {
            this.reservationsService = reservationsService;
            this.transfersRepository = transfersRepository;
        }

        public async Task<int> CancelExpiredReservationsAsync()
        {
            var reservations = await this.reservationsService.GetAllAwaitingReservationsAsync();
            var reservationsToCancel = reservations.Where(r => r.ReservationDate.CompareTo(DateTime.UtcNow) < 0
                                                          && r.Status != Status.Accomplished);

            var result = 0;

            foreach (var reservation in reservationsToCancel)
            {
                result = await this.reservationsService.CancelAsync(reservation.Id);
            }

            return result;
        }

        public async Task<int> SettingUpTransfersStatusByDateAsync()
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
