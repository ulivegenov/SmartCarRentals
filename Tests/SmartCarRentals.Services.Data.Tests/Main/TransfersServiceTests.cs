namespace SmartCarRentals.Services.Data.Tests.Main
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Repositories;
    using SmartCarRentals.Services.Data.Main;
    using SmartCarRentals.Services.Data.Tests.Common;
    using SmartCarRentals.Services.Data.Tests.Common.Seeders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class TransfersServiceTests
    {
        private const string ErrorMessage = "Method does not work properly!";

        [Theory]
        [InlineData("abc", 3, 0)]
        [InlineData("abc", 2, 0)]
        [InlineData("abc", 1, 0)]
        public async Task GetByUserAsync_ShouldReturnCorrectReservationsCountForUserByPage(string clientId, int? reservationsPerPage, int skip)
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var transferRepository = new EfDeletableEntityRepository<Transfer>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var transfersService = new TransfersService(transferRepository, userRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTransfersAsync(context);
            await seeder.SeedUsersAsync(context);

            var result = await transfersService.GetByUserAsync(clientId, reservationsPerPage, skip);

            Assert.True(result.ToList().Count == reservationsPerPage, ErrorMessage);
        }

        [Theory]
        [InlineData("abc", null, 0)]
        [InlineData("abc", 5, 0)]
        public async Task GetByUserAsync_ShouldReturnAllReservationsForUser(string clientId, int? reservationsPerPage, int skip)
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var transferRepository = new EfDeletableEntityRepository<Transfer>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var transfersService = new TransfersService(transferRepository, userRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTransfersAsync(context);
            await seeder.SeedUsersAsync(context);

            var result = await transfersService.GetByUserAsync(clientId, reservationsPerPage, skip);

            Assert.True(result.ToList().Count == 3, ErrorMessage);
        }

        [Fact]
        public async Task IsDriverAvailableByDate_ShouldRetutnTrue()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var transferRepository = new EfDeletableEntityRepository<Transfer>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var transfersService = new TransfersService(transferRepository, userRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTransfersAsync(context);

            var result = await transfersService.IsDriverAvailableByDate(DateTime.Now, "abc");

            Assert.True(result, ErrorMessage);
        }

        [Fact]
        public async Task IsDriverAvailableByDate_ShouldRetutnFalse()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var transferRepository = new EfDeletableEntityRepository<Transfer>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var transfersService = new TransfersService(transferRepository, userRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTransfersAsync(context);

            var result = await transfersService.IsDriverAvailableByDate(DateTime.Now, "bcd");

            Assert.False(result, ErrorMessage);
        }

        [Fact]
        public async Task VoteAsync_ShouldRetutnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var transferRepository = new EfDeletableEntityRepository<Transfer>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var transfersService = new TransfersService(transferRepository, userRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTransfersAsync(context);

            var result = await transfersService.VoteAsync(1);

            Assert.True(result == 1, ErrorMessage);
        }

        [Fact]
        public async Task PayByIdAsync_ShouldRetutnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var transferRepository = new EfDeletableEntityRepository<Transfer>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var transfersService = new TransfersService(transferRepository, userRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTransfersAsync(context);
            await seeder.SeedUsersAsync(context);

            var transfer = transferRepository.GetByIdWithDeletedAsync(3);
            var result = await transfersService.PayByIdAsync(3, "abc");

            Assert.True(result == 10, ErrorMessage);
        }
    }
}
