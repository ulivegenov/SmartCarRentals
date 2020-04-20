namespace SmartCarRentals.Services.Data.Tests.Common.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using SmartCarRentals.Data;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Data.Models.Enums.Reservation;

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
    }
}
