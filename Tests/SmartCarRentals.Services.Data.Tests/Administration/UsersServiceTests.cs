namespace SmartCarRentals.Services.Data.Tests.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http.Connections;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Repositories;
    using SmartCarRentals.Services.Data.Administration;
    using SmartCarRentals.Services.Data.Tests.Common;
    using SmartCarRentals.Services.Data.Tests.Common.Seeders;

    using Xunit;

    public class UsersServiceTests
    {
        private const string ErrorMessage = "Method does not work properly!";

        [Fact]
        public async Task GetCountAsync_ShouldResturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var roleStoreMock = new Mock<IRoleStore<ApplicationRole>>();

            var userManager = new UserManager<ApplicationUser>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var roleManager = new RoleManager<ApplicationRole>(roleStoreMock.Object, null, null, null, null);
            var usersService = new UsersService(usersRepository, roleManager, userManager);
            var seeder = new DbContextTestsSeeder();
            var count = await seeder.SeedUsersAsync(context);

            var expectedCount = await usersService.GetCountAsync();

            Assert.True(expectedCount == count, ErrorMessage);
        }

        [Fact]
        public async Task GetAllAsync_ShouldResturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var roleStoreMock = new Mock<IRoleStore<ApplicationRole>>();

            var userManager = new UserManager<ApplicationUser>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var roleManager = new RoleManager<ApplicationRole>(roleStoreMock.Object, null, null, null, null);
            var usersService = new UsersService(usersRepository, roleManager, userManager);
            var seeder = new DbContextTestsSeeder();
            var count = await seeder.SeedUsersAsync(context);

            var users = await usersService.GetAllAsync();

            Assert.True(users.ToList().Count == count, ErrorMessage);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldResturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            var roleStoreMock = new Mock<IRoleStore<ApplicationRole>>();

            var userManager = new UserManager<ApplicationUser>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            var roleManager = new RoleManager<ApplicationRole>(roleStoreMock.Object, null, null, null, null);
            var usersService = new UsersService(usersRepository, roleManager, userManager);
            var seeder = new DbContextTestsSeeder();
            var count = await seeder.SeedUsersAsync(context);

            var expectedId = await usersRepository.All().Select(u => u.Id).FirstOrDefaultAsync();
            var user = await usersService.GetByIdAsync(expectedId);

            Assert.True(user.Id == expectedId, ErrorMessage);
        }
    }
}
