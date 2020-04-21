namespace SmartCarRentals.Services.Data.Tests.Common.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using SmartCarRentals.Common;
    using SmartCarRentals.Data;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Data.Models.Enums.Reservation;
    using SmartCarRentals.Data.Models.Enums.User;

    public class DbContextTestsSeeder
    {
        public async Task<int> SeedCountriesAsync(ApplicationDbContext context)
        {
            var countries = new Country[]
            {
                new Country() { Name = "Bulgaria" },
                new Country() { Name = "Belgum" },
                new Country() { Name = "Germany" },
                new Country() { Name = "Italy" },
                new Country() { Name = "Romania" },
                new Country() { Name = "Greece" },
                new Country() { Name = "Poland" },
                new Country() { Name = "Turkey" },
            };

            foreach (var country in countries)
            {
                await context.Countries.AddAsync(country);
            }

            await context.SaveChangesAsync();

            return countries.Length;
        }

        public async Task<int> SeedCarsAsync(ApplicationDbContext context)
        {

            var trips = new List<Trip>();

            var countries = new Country[]
            {
                new Country() { Name = "Bulgaria" },
                new Country() { Name = "Greece" },
                new Country() { Name = "Romania" },
            };

            var parkings = new List<Parking>()
            {
                new Parking()
                {
                    Name = "Sofia one",
                    Address = "sdjhgfjh",
                    Town = new Town()
                    {
                        Name = "Sofia",
                        Country = countries[0],
                    },
                },

                new Parking()
                {
                    Name = "Athens one",
                    Address = "sdjhgfjh",
                    Town = new Town()
                    {
                        Name = "Athens",
                        Country = countries[1],
                    },
                },

                new Parking()
                {
                    Name = "Bucharest one",
                    Address = "sdjhgfjh",
                    Town = new Town()
                    {
                        Name = "Bucharest",
                        Country = countries[2],
                    },
                },
            };

            var cars = new Car[]
            {
                new Car()
                {
                    Image = "sdkjf",
                    Make = "Opel",
                    Model = "Corsa",
                    Class = ClassType.Compact,
                    HireStatus = HireStatus.Available,
                    PricePerDay = 20,
                    Rating = 9.0,
                    Trips = trips,
                    ParkingId = 1,
                    Parking = parkings[0],
                },

                new Car()
                {
                    Image = "sdkjf",
                    Make = "Audi",
                    Model = "A3",
                    Class = ClassType.Compact,
                    HireStatus = HireStatus.Available,
                    PricePerDay = 20,
                    Rating = 9.0,
                    Trips = trips,
                    ParkingId = 1,
                    Parking = parkings[0],
                },

                new Car()
                {
                    Image = "sdkjf",
                    Make = "Mercedes",
                    Model = "S 500",
                    Class = ClassType.SUVLimo,
                    HireStatus = HireStatus.Available,
                    PricePerDay = 20,
                    Rating = 9.0,
                    Trips = trips,
                    ParkingId = 1,
                    Parking = parkings[0],
                },

                new Car()
                {
                    Image = "sdkjf",
                    Make = "Audi",
                    Model = "A6",
                    Class = ClassType.Compact,
                    HireStatus = HireStatus.Available,
                    PricePerDay = 20,
                    Rating = 9.0,
                    Trips = trips,
                    ParkingId = 2,
                    Parking = parkings[1],
                },

                new Car()
                {
                    Image = "sdkjf",
                    Make = "BMW",
                    Model = "M5",
                    Class = ClassType.Compact,
                    HireStatus = HireStatus.Available,
                    PricePerDay = 20,
                    Rating = 9.0,
                    Trips = trips,
                    ParkingId = 3,
                    Parking = parkings[2],
                },
            };

            foreach (var car in cars)
            {
                await context.Cars.AddAsync(car);
            }

            await context.SaveChangesAsync();

            return cars.Length;
        }

        public async Task<int> SeedParkingsAsync(ApplicationDbContext context)
        {
            var parkings = new Parking[]
            {
                new Parking()
                {
                    Name = "Sofia one",
                    TownId = 1,
                    Town = new Town()
                    {
                        CountryId = 1,
                        Name = "Sofia",
                    },
                    Address = "Some street",
                    Capacity = 100,
                },

                new Parking()
                {
                    Name = "Athens one",
                    TownId = 2,
                    Town = new Town()
                    {
                        CountryId = 2,
                        Name = "Athens",
                    },
                    Address = "Some street",
                    Capacity = 100,
                },

                new Parking()
                {
                    Name = "Bucharest one",
                    TownId = 3,
                    Town = new Town()
                    {
                        CountryId = 3,
                        Name = "Bucharest",
                    },
                    Address = "Some street",
                    Capacity = 100,
                },
            };

            foreach (var parking in parkings)
            {
                await context.Parkings.AddAsync(parking);
            }

            await context.SaveChangesAsync();

            return parkings.Length;
        }

        public async Task<int> SeedTownsAsync(ApplicationDbContext context)
        {
            var towns = new Town[]
            {
                new Town()
                {
                    Name = "Sofia",
                    CountryId = 1,
                },

                new Town()
                {
                    Name = "Athens",
                    CountryId = 6,
                },

                new Town()
                {
                    Name = "Bucharest",
                    CountryId = 5,
                },
            };

            foreach (var town in towns)
            {
                await context.Towns.AddAsync(town);
            }

            await context.SaveChangesAsync();

            return towns.Length;
        }

        public async Task<int> SeedParkingSlotsAsync(ApplicationDbContext context)
        {
            var parkingSlots = new ParkingSlot[]
            {
                new ParkingSlot() { ParkingId = 1 },
                new ParkingSlot() { ParkingId = 1 },
                new ParkingSlot() { ParkingId = 1 },
                new ParkingSlot() { ParkingId = 1 },
                new ParkingSlot() { ParkingId = 2 },
                new ParkingSlot() { ParkingId = 2 },
                new ParkingSlot() { ParkingId = 2 },
                new ParkingSlot() { ParkingId = 3 },
                new ParkingSlot() { ParkingId = 3 },
            };

            foreach (var parkingSlot in parkingSlots)
            {
                await context.ParkingSlots.AddAsync(parkingSlot);
            }

            await context.SaveChangesAsync();

            return parkingSlots.Length;
        }

        public async Task<int> SeedDriversRatings(ApplicationDbContext context)
        {
            var driversRatings = new DriverRating[]
            {
                new DriverRating() { ClientId = "dta", TransferId = 1, DriverId = "abc" },
                new DriverRating() { ClientId = "dta", TransferId = 2, DriverId = "abc" },
                new DriverRating() { ClientId = "dta", TransferId = 3, DriverId = "abc" },
                new DriverRating() { ClientId = "dta", TransferId = 4, DriverId = "bcd" },
                new DriverRating() { ClientId = "dta", TransferId = 5, DriverId = "bcd" },
                new DriverRating() { ClientId = "dta", TransferId = 6, DriverId = "cde" },
            };

            foreach (var driverRating in driversRatings)
            {
                await context.DriversRatings.AddAsync(driverRating);
            }

            await context.SaveChangesAsync();

            return driversRatings.Length;
        }

        public async Task<int> SeedReservationsAsync(ApplicationDbContext context)
        {
            var town = new Town() { Name = "TownName" };

            var car1 = new Car()
            {
                Id = "abc",
                Make = "Make",
                Model = "Model",
                PlateNumber = "Number",
            };

            var car2 = new Car()
            {
                Id = "bcd",
                Make = "Make",
                Model = "Model",
                PlateNumber = "Number",
            };

            var car3 = new Car()
            {
                Id = "cde",
                Make = "Make",
                Model = "Model",
                PlateNumber = "Number",
            };

            var parking = new Parking()
            {
                Address = "Address",
                Town = town,
            };

            var reservations = new Reservation[]
            {
                new Reservation()
                {
                    ClientId = "abc",
                    CarId = "abc",
                    Car = car1,
                    ReservationDate = DateTime.UtcNow.AddDays(5),
                    Status = Status.Awaiting,
                    Parking = parking,
                    ParkingId = 1,
                },

                new Reservation()
                {
                    ClientId = "abc",
                    CarId = "abc",
                    Car = car1,
                    ReservationDate = DateTime.UtcNow.AddDays(6),
                    Status = Status.Awaiting,
                    Parking = parking,
                    ParkingId = 1,
                },

                new Reservation()
                {
                    ClientId = "abc",
                    CarId = "abc",
                    Car = car1,
                    ReservationDate = DateTime.UtcNow.AddDays(2),
                    Status = Status.Awaiting,
                    Parking = parking,
                    ParkingId = 1,
                },

                new Reservation()
                {
                    ClientId = "bcd",
                    CarId = "bcd",
                    Car = car2,
                    ReservationDate = DateTime.UtcNow.AddDays(5),
                    Status = Status.Awaiting,
                    Parking = parking,
                    ParkingId = 1,
                },

                new Reservation()
                {
                    ClientId = "cde",
                    CarId = "cde",
                    Car = car3,
                    ReservationDate = DateTime.UtcNow.AddDays(5),
                    Status = Status.Awaiting,
                    Parking = parking,
                    ParkingId = 1,
                },

                new Reservation()
                {
                    ClientId = "cde",
                    CarId = "cde",
                    Car = car3,
                    ReservationDate = DateTime.UtcNow.AddDays(-5),
                    Status = Status.Accomplished,
                    Parking = parking,
                    ParkingId = 1,
                },

                new Reservation()
                {
                    ClientId = "cde",
                    CarId = "cde",
                    Car = car3,
                    ReservationDate = DateTime.UtcNow.AddDays(-10),
                    Status = Status.Canceled,
                    Parking = parking,
                    ParkingId = 1,
                },

                new Reservation()
                {
                    ClientId = "cde",
                    Car = car3,
                    CarId = "cde",
                    ReservationDate = DateTime.UtcNow,
                    Status = Status.Awaiting,
                    Parking = parking,
                    ParkingId = 1,
                },
            };

            foreach (var reservation in reservations)
            {
                await context.Reservations.AddAsync(reservation);
            }

            await context.SaveChangesAsync();

            return reservations.Length;
        }

        public async Task<int> SeedTransfersAsync(ApplicationDbContext context)
        {
            var driver1 = new Driver()
            {
                Id = "abc",
                FirstName = "FirstName",
                LastName = "LastName",
            };

            var driver2 = new Driver()
            {
                Id = "bcd",
                FirstName = "FirstName",
                LastName = "LastName",
            };

            var transferType = new TransferType()
            {
                Name = "Transfer",
                Price = 100,
            };

            var transfers = new Transfer[]
            {
                new Transfer()
                {
                    ClientId = "abc",
                    TransferDate = DateTime.UtcNow.AddDays(10),
                    Status = SmartCarRentals.Data.Models.Enums.Transfer.Status.Forthcoming,
                    Type = transferType,
                    Driver = driver1,
                    HasPaid = false,
                    HasVote = false,
                },

                new Transfer()
                {
                    ClientId = "abc",
                    TransferDate = DateTime.UtcNow.AddDays(5),
                    Status = SmartCarRentals.Data.Models.Enums.Transfer.Status.Forthcoming,
                    Type = transferType,
                    Driver = driver1,
                    HasPaid = false,
                    HasVote = false,
                },

                new Transfer()
                {
                    ClientId = "abc",
                    TransferDate = DateTime.UtcNow.AddDays(-10),
                    Status = SmartCarRentals.Data.Models.Enums.Transfer.Status.Finished,
                    Type = transferType,
                    Driver = driver1,
                    HasPaid = false,
                    HasVote = false,
                    Points = 0,
                },

                new Transfer()
                {
                    ClientId = "bcd",
                    TransferDate = DateTime.UtcNow.AddDays(-10),
                    Status = SmartCarRentals.Data.Models.Enums.Transfer.Status.Finished,
                    Type = transferType,
                    Driver = driver2,
                    HasPaid = true,
                    HasVote = true,
                },

                new Transfer()
                {
                    ClientId = "bcd",
                    TransferDate = DateTime.UtcNow.AddDays(-3),
                    Status = SmartCarRentals.Data.Models.Enums.Transfer.Status.Finished,
                    Type = transferType,
                    Driver = driver2,
                    HasPaid = true,
                    HasVote = true,
                },

                new Transfer()
                {
                    ClientId = "cde",
                    TransferDate = DateTime.UtcNow.AddDays(-10),
                    Status = SmartCarRentals.Data.Models.Enums.Transfer.Status.Finished,
                    Type = transferType,
                    Driver = driver1,
                    HasPaid = true,
                    HasVote = true,
                },

                new Transfer()
                {
                    ClientId = "def",
                    TransferDate = DateTime.UtcNow,
                    Status = SmartCarRentals.Data.Models.Enums.Transfer.Status.OnGoing,
                    Type = transferType,
                    Driver = driver2,
                    HasPaid = true,
                    HasVote = true,
                },

                new Transfer()
                {
                    ClientId = "efg",
                    TransferDate = DateTime.UtcNow,
                    Status = SmartCarRentals.Data.Models.Enums.Transfer.Status.OnGoing,
                    Type = transferType,
                    Driver = driver2,
                    HasPaid = true,
                    HasVote = true,
                },
            };

            foreach (var transfer in transfers)
            {
                await context.Transfers.AddAsync(transfer);
            }

            await context.SaveChangesAsync();

            return transfers.Length;
        }

        public async Task<int> SeedUsersAsync(ApplicationDbContext context)
        {
            var users = new ApplicationUser[]
            {
                new ApplicationUser()
                {
                    Id = "abc",
                    FirstName = "FirstName",
                    LastName = "LastName",
                },

                new ApplicationUser()
                {
                    Id = "bcd",
                    FirstName = "FirstName",
                    LastName = "LastName",
                },

                new ApplicationUser()
                {
                    Id = "cde",
                    FirstName = "FirstName",
                    LastName = "LastName",
                },

                new ApplicationUser()
                {
                    Id = "def",
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Rank = RankType.GoldUser,
                },

                new ApplicationUser()
                {
                    Id = "efg",
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Rank = RankType.PlatinumUser,
                },
            };

            foreach (var user in users)
            {
                await context.Users.AddAsync(user);
            }

            await context.SaveChangesAsync();

            return users.Length;
        }

        public async Task<int> SeedTripsAsync(ApplicationDbContext context)
        {
            var car1 = new Car()
            {
                Make = "Make",
                Model = "Model",
                HireStatus = HireStatus.Unavailable,
                ParkingId = 1,
                PlateNumber = "PlateNumber",
                PricePerDay = 30,
                PricePerHour = 3,
            };

            var car2 = new Car()
            {
                Make = "Make",
                Model = "Model",
                HireStatus = HireStatus.Unavailable,
                ParkingId = 1,
                PlateNumber = "PlateNumber",
                PricePerDay = 30,
                PricePerHour = 3,
            };

            var car3 = new Car()
            {
                Make = "Make",
                Model = "Model",
                HireStatus = HireStatus.Unavailable,
                ParkingId = 1,
                PlateNumber = "PlateNumber",
                PricePerDay = 30,
                PricePerHour = 3,
            };

            var trips = new Trip[]
            {
                new Trip()
                {
                    ClientId = "abc",
                    EndDate = null,
                    KmRun = null,
                    CarId = "abc",
                    Car = car1,
                    HasPaid = false,
                    HasVote = false,
                    Points = 0,
                    Price = 0,
                    Status = SmartCarRentals.Data.Models.Enums.Trip.Status.OnGoing,
                },

                new Trip()
                {
                    CreatedOn = DateTime.UtcNow.AddDays(-2),
                    ClientId = "abc",
                    EndDate = null,
                    KmRun = null,
                    CarId = "bcd",
                    Car = car2,
                    HasPaid = true,
                    HasVote = false,
                    Points = 0,
                    Price = 0,
                    Status = SmartCarRentals.Data.Models.Enums.Trip.Status.Finished,
                },

                new Trip()
                {
                    ClientId = "bcd",
                    EndDate = null,
                    KmRun = null,
                    CarId = "bcd",
                    Car = car2,
                    HasPaid = false,
                    HasVote = false,
                    Points = 0,
                    Price = 0,
                    Status = SmartCarRentals.Data.Models.Enums.Trip.Status.OnGoing,
                },

                new Trip()
                {
                    ClientId = "cde",
                    EndDate = null,
                    KmRun = null,
                    CarId = "cde",
                    Car = car3,
                    HasPaid = false,
                    HasVote = false,
                    Points = 0,
                    Price = 0,
                    Status = SmartCarRentals.Data.Models.Enums.Trip.Status.OnGoing,
                },

                new Trip()
                {
                    CreatedOn = DateTime.UtcNow.AddHours(-6),
                    ClientId = "cde",
                    EndDate = null,
                    KmRun = null,
                    CarId = "bcd",
                    Car = car3,
                    HasPaid = true,
                    HasVote = false,
                    Points = 0,
                    Price = 0,
                    Status = SmartCarRentals.Data.Models.Enums.Trip.Status.Finished,
                },

                new Trip()
                {
                    CreatedOn = DateTime.UtcNow.AddDays(-2),
                    ClientId = "def",
                    EndDate = null,
                    KmRun = null,
                    CarId = "bcd",
                    Car = car2,
                    HasPaid = true,
                    HasVote = false,
                    Points = 0,
                    Price = 0,
                    Status = SmartCarRentals.Data.Models.Enums.Trip.Status.Finished,
                },

                new Trip()
                {
                    CreatedOn = DateTime.UtcNow.AddDays(-2),
                    ClientId = "efg",
                    EndDate = null,
                    KmRun = null,
                    CarId = "bcd",
                    Car = car2,
                    HasPaid = true,
                    HasVote = false,
                    Points = 0,
                    Price = 0,
                    Status = SmartCarRentals.Data.Models.Enums.Trip.Status.Finished,
                },

                new Trip()
                {
                    CreatedOn = DateTime.UtcNow.AddHours(-6),
                    ClientId = "def",
                    EndDate = null,
                    KmRun = null,
                    CarId = "bcd",
                    Car = car3,
                    HasPaid = true,
                    HasVote = false,
                    Points = 0,
                    Price = 0,
                    Status = SmartCarRentals.Data.Models.Enums.Trip.Status.Finished,
                },

                new Trip()
                {
                    CreatedOn = DateTime.UtcNow.AddHours(-6),
                    ClientId = "efg",
                    EndDate = null,
                    KmRun = null,
                    CarId = "bcd",
                    Car = car3,
                    HasPaid = true,
                    HasVote = false,
                    Points = 0,
                    Price = 0,
                    Status = SmartCarRentals.Data.Models.Enums.Trip.Status.Finished,
                },
            };

            foreach (var trip in trips)
            {
                await context.Trips.AddAsync(trip);
            }

            await context.SaveChangesAsync();

            return trips.Length;
        }
    }
}
