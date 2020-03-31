namespace SmartCarRentals.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.Main.Contacts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.DraversRatings;
    using SmartCarRentals.Web.ViewModels.Main.DriversRatings;

    public class DriversRatingsController : BaseController
    {
        private readonly IDriversRatingsService driversRatingsService;
        private readonly ITransfersService transfersService;
        private readonly UserManager<ApplicationUser> userManager;

        public DriversRatingsController(
                                        IDriversRatingsService driversRatingsService,
                                        ITransfersService transfersService,
                                        UserManager<ApplicationUser> userManager)
        {
            this.driversRatingsService = driversRatingsService;
            this.transfersService = transfersService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create(int transferId)
        {
            var viewModel = new DriverRatingCreateInputModel()
            {
                TransferId = transferId,
            };
            return this.View(viewModel);
        }

        // TODO REFACTORING - driver.Id is null???
        [Authorize]
        [Route("/DriversRatings/Create/{id}")]
        public async Task<IActionResult> Create(DriverRatingCreateInputModel driverRatingCreateInputModel, int transferId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var transfer = await this.transfersService.GetByIdAsync(transferId);

            if (!this.ModelState.IsValid)
            {
                driverRatingCreateInputModel.ClientId = currentUser.Id;
                driverRatingCreateInputModel.TransferId = transferId;
                driverRatingCreateInputModel.DriverId = transfer.Driver.Id;

                return this.View(driverRatingCreateInputModel);
            }

            driverRatingCreateInputModel.ClientId = currentUser.Id;
            driverRatingCreateInputModel.TransferId = transferId;
            driverRatingCreateInputModel.DriverId = transfer.Driver.Id;

            var serviceModel = driverRatingCreateInputModel.To<DriverRatingServiceInputModel>();

            await this.driversRatingsService.CreateAsync(serviceModel);

            return this.Redirect("/Transfers/MyTransfers");
        }
    }
}
