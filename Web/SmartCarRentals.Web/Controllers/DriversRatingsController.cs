namespace SmartCarRentals.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.DraversRatings;
    using SmartCarRentals.Web.ViewModels.Main.DriversRatings;

    public class DriversRatingsController : BaseController
    {
        private readonly IDriversRatingsService driversRatingsService;
        private readonly ITransfersService transfersService;
        private readonly IDriversService driversService;
        private readonly UserManager<ApplicationUser> userManager;

        public DriversRatingsController(
                                        IDriversRatingsService driversRatingsService,
                                        ITransfersService transfersService,
                                        IDriversService driversService,
                                        UserManager<ApplicationUser> userManager)
        {
            this.driversRatingsService = driversRatingsService;
            this.transfersService = transfersService;
            this.driversService = driversService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Create(int id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var transferServiceModel = await this.transfersService.GetByIdAsync(id);
            var transfer = transferServiceModel.To<Transfer>();
            var draver = transfer.Driver;
            var viewModel = new DriverRatingCreateInputModel()
            {
                Client = currentUser,
                ClientId = currentUser.Id,
                Transfer = transfer,
                TransferId = id,
                DriverId = draver.Id,
            };
            return this.View(viewModel);
        }

        [Authorize]
        [Route("/DriversRatings/Create/{id}")]
        [HttpPost]
        public async Task<IActionResult> Create(DriverRatingCreateInputModel driverRatingCreateInputModel, int id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var transfer = await this.transfersService.GetByIdAsync(id);
            var driver = transfer.Driver;

            if (!this.ModelState.IsValid)
            {
                driverRatingCreateInputModel.ClientId = currentUser.Id;
                driverRatingCreateInputModel.TransferId = id;
                driverRatingCreateInputModel.DriverId = driver.Id;

                return this.View(driverRatingCreateInputModel);
            }

            driverRatingCreateInputModel.ClientId = currentUser.Id;
            driverRatingCreateInputModel.TransferId = id;
            driverRatingCreateInputModel.DriverId = driver.Id;

            var serviceModel = driverRatingCreateInputModel.To<DriverRatingServiceInputModel>();

            await this.driversRatingsService.CreateAsync(serviceModel);

            await this.transfersService.VoteAsync(id);

            return this.Redirect("/Transfers/MyTransfers");
        }
    }
}
