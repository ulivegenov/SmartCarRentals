namespace SmartCarRentals.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.Main.Contacts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Drivers;
    using SmartCarRentals.Services.Models.Main.Transfers;
    using SmartCarRentals.Services.Models.Main.TransfersTypes;
    using SmartCarRentals.Web.ViewModels.Administration.Drivers;
    using SmartCarRentals.Web.ViewModels.Main.DriversRatings;
    using SmartCarRentals.Web.ViewModels.Main.Transfers;
    using SmartCarRentals.Web.ViewModels.Main.TransfersTypes;

    public class TransfersController : BaseController
    {
        private readonly ITransfersService transfersService;
        private readonly IDriversService driversService;
        private readonly ITransfersTypesService transfersTypesService;
        private readonly UserManager<ApplicationUser> userManager;

        public TransfersController(
                                   ITransfersService transfersService,
                                   IDriversService driversService,
                                   ITransfersTypesService transfersTypesService,
                                   UserManager<ApplicationUser> userManager)
        {
            this.transfersService = transfersService;
            this.driversService = driversService;
            this.transfersTypesService = transfersTypesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create(string searchByDriver)
        {
            var drivers = await this.driversService.GetAllAsync<DriverServiceDetailsModel>();
            var transfersTypes = await this.transfersTypesService.GetAllAsync<TransfersTypesServiceDropDownModel>();

            this.ViewData["DriverFilter"] = searchByDriver;

            if (!string.IsNullOrEmpty(searchByDriver))
            {
                drivers = drivers.Where(d => $"{d.FirstName} {d.LastName}" == searchByDriver);
            }

            var viewModel = new TransferCreateInputModel()
            {
                Drivers = drivers.Select(d => d.To<DriverDetailsViewModel>()).ToList(),
                TransfersTypes = transfersTypes.Select(t => t.To<TransfersTypesDropDownViewModel>()).ToList(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(TransferCreateInputModel transferCreateInputModel, string searchByDriver)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            transferCreateInputModel.ClientId = currentUser.Id;

            if (!this.ModelState.IsValid)
            {
                var drivers = await this.driversService.GetAllAsync<DriverServiceDetailsModel>();
                var transfersTypes = await this.transfersTypesService.GetAllAsync<TransfersTypesServiceDropDownModel>();

                this.ViewData["DriverFilter"] = searchByDriver;

                if (!string.IsNullOrEmpty(searchByDriver))
                {
                    drivers = drivers.Where(d => $"{d.FirstName} {d.LastName}" == searchByDriver);
                }

                transferCreateInputModel.Drivers = drivers.Select(d => d.To<DriverDetailsViewModel>()).ToList();
                transferCreateInputModel.TransfersTypes = transfersTypes.Select(t => t.To<TransfersTypesDropDownViewModel>()).ToList();
                transferCreateInputModel.ClientId = currentUser.Id;

                return this.View(transferCreateInputModel);
            }

            var serviceTransfer = transferCreateInputModel.To<TransferServiceInputModel>();

            await this.transfersService.CreateAsync(serviceTransfer);

            return this.Redirect("/");
        }

        [HttpGet("/Transfers/Create/{driverId}")]
        public async Task<IActionResult> CreateByDriver(string driverId)
        {
            var driverService = await this.driversService.GetByIdAsync<DriverServiceDetailsModel>(driverId);
            var transfersTypes = await this.transfersTypesService.GetAllAsync<TransfersTypesServiceDropDownModel>();

            var viewModel = new TransferCreateInputModel()
            {
                Driver = driverService.To<Driver>(),
                TransfersTypes = transfersTypes.Select(t => t.To<TransfersTypesDropDownViewModel>()).ToList(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost("/Transfers/Create/{driverId}")]
        public async Task<IActionResult> CreateByDriver(TransferCreateInputModel transferCreateInputModel, string driverId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                var driverService = await this.driversService.GetByIdAsync<DriverServiceDetailsModel>(driverId);
                var transfersTypes = await this.transfersTypesService.GetAllAsync<TransfersTypesServiceDropDownModel>();
                transferCreateInputModel.Driver = driverService.To<Driver>();
                transferCreateInputModel.TransfersTypes = transfersTypes.Select(t => t.To<TransfersTypesDropDownViewModel>()).ToList();
                transferCreateInputModel.ClientId = currentUser.Id;

                return this.View(transferCreateInputModel);
            }

            var serviceTransfer = transferCreateInputModel.To<TransferServiceInputModel>();
            serviceTransfer.ClientId = currentUser.Id;

            await this.transfersService.CreateAsync(serviceTransfer);

            return this.Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> MyTransfers()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var transfers = await this.transfersService.GetByUser(user.Id);

            var viewModel = new MyTransfersAllViewModelCollection();
            viewModel.MyTransfers = transfers.Select(t => t.To<MyTransfersAllViewModel>()).ToList();

            return this.View(viewModel);
        }
    }
}
