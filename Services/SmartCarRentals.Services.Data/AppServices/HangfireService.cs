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

    public class HangfireService : IHangfireService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationsRepository;
        private readonly IDeletableEntityRepository<Transfer> transfersRepository;

        public HangfireService(
                               IDeletableEntityRepository<Reservation> reservationsRepository,
                               IDeletableEntityRepository<Transfer> transfersRepository)
        {
            this.reservationsRepository = reservationsRepository;
            this.transfersRepository = transfersRepository;
        }

        public async Task<int> CancelExpiredReservationsAsync()
        {
            var reservationsToCancel = await this.reservationsRepository.All()
                                                                        .Where(r => r.Status == Status.Awaiting
                                                                               && r.ReservationDate.Date.CompareTo(DateTime.UtcNow.Date) < 0)
                                                                        .ToListAsync();

            foreach (var reservation in reservationsToCancel)
            {
                reservation.Status = Status.Canceled;
                this.reservationsRepository.Update(reservation);
            }

            var result = await this.reservationsRepository.SaveChangesAsync();

            return result;
        }

        public async Task<int> SettingUpTransfersStatusByDateAsync()
        {
            var transfers = await this.transfersRepository.All()
                                                          .Where(t => t.TransferDate.Date.CompareTo(DateTime.UtcNow.Date) == 0
                                                                 && t.Status == SmartCarRentals.Data.Models.Enums.Transfer.Status.Forthcoming)
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
