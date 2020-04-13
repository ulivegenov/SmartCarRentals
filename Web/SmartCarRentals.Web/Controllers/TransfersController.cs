namespace SmartCarRentals.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Hangfire;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.AppServices.Contracts;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Drivers;
    using SmartCarRentals.Services.Models.Main.Transfers;
    using SmartCarRentals.Services.Models.Main.TransfersTypes;
    using SmartCarRentals.Web.ViewModels.Administration.Drivers;
    using SmartCarRentals.Web.ViewModels.Main.Transfers;
    using SmartCarRentals.Web.ViewModels.Main.TransfersTypes;

    public class TransfersController : BaseController
    {
        private const string UnavailableDriverErrorMessage = "Sorry! This Driver is unavailable, for this date.";

        private readonly ITransfersService transfersService;
        private readonly IDriversService driversService;
        private readonly ITransfersTypesService transfersTypesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;
        private readonly IBackgroundJobClient backgroungJobClient;
        private readonly IHangfireService hangfireService;

        public TransfersController(
                                   ITransfersService transfersService,
                                   IDriversService driversService,
                                   ITransfersTypesService transfersTypesService,
                                   UserManager<ApplicationUser> userManager,
                                   IUsersService usersService,
                                   IBackgroundJobClient backgroungJobClient,
                                   IHangfireService hangfireService)
        {
            this.transfersService = transfersService;
            this.driversService = driversService;
            this.transfersTypesService = transfersTypesService;
            this.userManager = userManager;
            this.usersService = usersService;
            this.backgroungJobClient = backgroungJobClient;
            this.hangfireService = hangfireService;
        }

        public async Task<IActionResult> Create(string searchByDriver)
        {
            var drivers = await this.driversService.GetAllAsync<DriverServiceDetailsModel>();
            var transfersTypes = await this.transfersTypesService.GetAllAsync<TransfersTypesServiceDropDownModel>();

            this.ViewData["DriverFilter"] = searchByDriver;

            if (!string.IsNullOrEmpty(searchByDriver))
            {
                drivers = drivers.Where(d => d.Id == searchByDriver);
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
            var driverServiceModel = await this.driversService.GetByIdAsync<DriverServiceDetailsModel>(transferCreateInputModel.DriverId);

            if (!this.ModelState.IsValid)
            {
                var drivers = await this.driversService.GetAllAsync<DriverServiceDetailsModel>();
                var transfersTypes = await this.transfersTypesService.GetAllAsync<TransfersTypesServiceDropDownModel>();

                this.ViewData["DriverFilter"] = searchByDriver;

                if (!string.IsNullOrEmpty(searchByDriver))
                {
                    drivers = drivers.Where(d => d.Id == searchByDriver);
                }

                transferCreateInputModel.Drivers = drivers.Select(d => d.To<DriverDetailsViewModel>()).ToList();
                transferCreateInputModel.TransfersTypes = transfersTypes.Select(t => t.To<TransfersTypesDropDownViewModel>()).ToList();
                transferCreateInputModel.ClientId = currentUser.Id;

                return this.View(transferCreateInputModel);
            }

            if (!await this.driversService.IsDriverAvailableByDate(transferCreateInputModel.TransferDate, transferCreateInputModel.DriverId))
            {
                this.TempData["Error"] = UnavailableDriverErrorMessage;

                var drivers = await this.driversService.GetAllAsync<DriverServiceDetailsModel>();
                var transfersTypes = await this.transfersTypesService.GetAllAsync<TransfersTypesServiceDropDownModel>();

                this.ViewData["DriverFilter"] = searchByDriver;

                transferCreateInputModel.Drivers = drivers.Select(d => d.To<DriverDetailsViewModel>()).ToList();
                transferCreateInputModel.TransfersTypes = transfersTypes.Select(t => t.To<TransfersTypesDropDownViewModel>()).ToList();
                transferCreateInputModel.ClientId = currentUser.Id;

                return this.View(transferCreateInputModel);
            }

            var serviceTransfer = transferCreateInputModel.To<TransferServiceInputModel>();

            await this.transfersService.CreateAsync(serviceTransfer);

            return this.Redirect("/Transfers/MyTransfers");
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
            transferCreateInputModel.ClientId = currentUser.Id;
            var driverServiceModel = await this.driversService.GetByIdAsync<DriverServiceDetailsModel>(driverId);

            if (!this.ModelState.IsValid)
            {
                var transfersTypes = await this.transfersTypesService.GetAllAsync<TransfersTypesServiceDropDownModel>();
                transferCreateInputModel.Driver = driverServiceModel.To<Driver>();
                transferCreateInputModel.TransfersTypes = transfersTypes.Select(t => t.To<TransfersTypesDropDownViewModel>()).ToList();
                transferCreateInputModel.ClientId = currentUser.Id;

                return this.View(transferCreateInputModel);
            }

            if (!await this.driversService.IsDriverAvailableByDate(transferCreateInputModel.TransferDate, transferCreateInputModel.DriverId))
            {
                this.TempData["Error"] = UnavailableDriverErrorMessage;

                var transfersTypes = await this.transfersTypesService.GetAllAsync<TransfersTypesServiceDropDownModel>();
                transferCreateInputModel.Driver = driverServiceModel.To<Driver>();
                transferCreateInputModel.TransfersTypes = transfersTypes.Select(t => t.To<TransfersTypesDropDownViewModel>()).ToList();
                transferCreateInputModel.ClientId = currentUser.Id;

                return this.View(transferCreateInputModel); // TODO ERROR MESSAGE VIEW!!!
            }

            var serviceTransfer = transferCreateInputModel.To<TransferServiceInputModel>();
            await this.transfersService.CreateAsync(serviceTransfer);

            return this.Redirect("/Transfers/MyTransfers");
        }

        [Authorize]
        public async Task<IActionResult> MyTransfers(int id = 1)
        {
            var page = id;
            var user = await this.userManager.GetUserAsync(this.User);
            var transfers = await this.transfersService.GetByUserAsync(user.Id, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);

            var viewModel = new MyTransfersAllViewModelCollection();
            viewModel.MyTransfers = transfers.Select(t => t.To<MyTransfersAllViewModel>()).ToList();

            var count = await this.transfersService.GetCountAsync();
            viewModel.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.UsersPerPageAdmin);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost("/Transfers/Pay/{id}")]
        public async Task<IActionResult> Pay(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var transferPoints = await this.transfersService.PayByIdAsync(id, user.Id);

            await this.usersService.GetPointsAsync(user.Id, transferPoints);

            return this.Redirect("/Transfers/MyTransfers");
        }
    }
}
