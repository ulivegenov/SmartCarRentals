namespace SmartCarRentals.Services.Data.Tests.Common.Seeders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;

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
    }
}
