namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.TransfersTypes;
    using SmartCarRentals.Web.ViewModels.Administration.TransfersTypes;

    public class TransfersTypesController : AdministrationController
    {
        private const string DeleteErrorMessage = "Failed to delete the transfer type.";

        private readonly ITransfersTypesService transfersTypesService;

        public TransfersTypesController(ITransfersTypesService transfersTypesService)
        {
            this.transfersTypesService = transfersTypesService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var transferType = await this.transfersTypesService.GetByIdAsync<TransferTypeServiceDetailsModel>(id);
            var viewModel = transferType.To<TransferTypeEditInputModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TransferTypeEditInputModel transferTypeEditImputModel)
        {
            var serviceTransferType = transferTypeEditImputModel.To<TransferTypeServiceDetailsModel>();

            await this.transfersTypesService.EditAsync(serviceTransferType);

            return this.Redirect($"/Administration/TransfersTypes/Details/{serviceTransferType.Id}");
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransferTypeInputModel transferTypeInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(transferTypeInputModel);
            }

            var transferTypeServiceModel = transferTypeInputModel.To<TransferTypeServiceInputModel>();

            await this.transfersTypesService.CreateAsync(transferTypeServiceModel);

            return this.Redirect("/Administration/TransfersTypes/All");
        }

        public async Task<IActionResult> Details(int id)
        {
            var transferType = await this.transfersTypesService.GetByIdAsync<TransferTypeServiceDetailsModel>(id);
            var viewModel = transferType.To<TransferTypeDetailsViewModel>();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var transferType = await this.transfersTypesService.GetByIdAsync<TransferTypeServiceDetailsModel>(id);
            var viewModel = transferType.To<TransferTypeDetailsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("/Administration/TransfersTypes/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            if (!(await this.transfersTypesService.DeleteByIdAsync(id) > 0))
            {
                this.TempData["Error"] = DeleteErrorMessage;

                return this.View(); // TODO ERROR MESSAGE VIEW!!!
            }

            return this.Redirect("/Administration/TransfersTypes/All");
        }

        public async Task<IActionResult> All()
        {
            var transfersTypes = await this.transfersTypesService.GetAllAsync<TransfersTypesServiceAllModel>();

            var viewModel = new TransfersTypesAllViewModelCollection()
            {
                TransfersTypes = transfersTypes.Select(t => t.To<TransfersTypesAllViewModel>()).ToList(),
            };

            return this.View(viewModel);
        }
    }
}
