namespace SmartCarRentals.Services.Data.Main
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.Reservations;

    public class ReservationsService : BaseService<Reservation, int>, IReservationsService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationRepository;

        public ReservationsService(IDeletableEntityRepository<Reservation> reservationRepository)
            : base(reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<MyReservationsServiceAllModel>> GetByUserAsync(string userId)
        {
            var reservations = await this.reservationRepository.All()
                                                               .Where(r => r.ClientId == userId)
                                                               .Select(r => new Reservation()
                                                               {
                                                                   Id = r.Id,
                                                                   CarId = r.CarId,
                                                                   Car = new Car()
                                                                   {
                                                                       Make = r.Car.Make,
                                                                       Model = r.Car.Model,
                                                                       PlateNumber = r.Car.PlateNumber,
                                                                   },
                                                                   ReservationDate = r.ReservationDate,
                                                                   Status = r.Status,
                                                                   Parking = new Parking()
                                                                   {
                                                                       Address = r.Parking.Address,
                                                                       Town = new Town()
                                                                       {
                                                                           Name = r.Parking.Town.Name,
                                                                       },
                                                                   },
                                                               })
                                                               .OrderBy(r => r.Status)
                                                               .To<MyReservationsServiceAllModel>()
                                                               .ToListAsync();

            return reservations;
        }
    }
}
