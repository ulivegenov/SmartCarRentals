namespace SmartCarRentals.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Cars;
    using SmartCarRentals.Services.Models.Administration.Countries;
    using SmartCarRentals.Services.Models.Administration.Parkings;
    using SmartCarRentals.Services.Models.Administration.Towns;
    using SmartCarRentals.Web.ViewModels.Administration.Cars;
    using SmartCarRentals.Web.ViewModels.Administration.Parkings;

    public class CarsController : BaseController
    {
        private readonly ICarsService carsService;
        private readonly IParkingsService parkingsService;
        private readonly ITownsService townsService;
        private readonly ICountriesService countriesService;

        public CarsController(
                              ICarsService carsService,
                              IParkingsService parkingsService,
                              ITownsService townsService,
                              ICountriesService countriesService)
        {
            this.carsService = carsService;
            this.parkingsService = parkingsService;
            this.townsService = townsService;
            this.countriesService = countriesService;
        }

        public async Task<IActionResult> Details(string id)
        {
            var car = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(id);
            var carParking = await this.parkingsService.GetByCarIdAsync(id);
            car.Parking = carParking;
            var viewModel = car.To<CarDetailsViewModel>();

            return this.View(viewModel);
        }

        public async Task<IActionResult> All(string currentFilter, string searchString, int id = 1)
        {
            var page = id;
            var cars = this.carsService.GetAllWithPaging<CarsServiceAllModel>(GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
            var countries = await this.countriesService.GetAllAsync<CountriesServiceDropDownModel>();
            var towns = await this.townsService.GetAllAsync<TownsServiceDropDownModel>();
            var parkings = await this.parkingsService.GetAllAsync<ParkingsServiceDropDownModel>();

            var count = await this.carsService.GetCountAsync();

            var viewModel = new CarsAllViewModelCollection();
            var carsAllViewModel = cars.Select(c => c.To<CarsAllViewModel>()).ToList();
            viewModel.Countries = countries.Select(c => c.Name).ToList();
            viewModel.Towns = towns.Select(t => t.Name).ToList();
            viewModel.Parkings = parkings.Select(p => p.Name).ToList();

            if (!string.IsNullOrEmpty(searchString) && viewModel.Countries.Contains(searchString))
            {
                if (searchString != (string)this.TempData["SearchString"])
                {
                    id = 1;
                    this.TempData["SearchString"] = searchString;
                }

                page = id;
                cars = await this.carsService.GetAllByCountryWithPagingAsync<CarsServiceAllModel>
                    (searchString, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
                count = await this.carsService.GetCountByCountryAsync(searchString);
            }
            else if (!string.IsNullOrEmpty(searchString) && viewModel.Towns.Contains(searchString))
            {
                if (searchString != (string)this.TempData["SearchString"])
                {
                    id = 1;
                    this.TempData["SearchString"] = searchString;
                }

                page = id;
                cars = await this.carsService.GetAllByTownWithPagingAsync<CarsServiceAllModel>
                    (searchString, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
                count = await this.carsService.GetCountByTownAsync(searchString);
            }
            else if (!string.IsNullOrEmpty(searchString) && viewModel.Parkings.Contains(searchString))
            {
                if (searchString != (string)this.TempData["SearchString"])
                {
                    id = 1;
                    this.TempData["SearchString"] = searchString;
                }

                page = id;
                cars = await this.carsService.GetAllByParkingWithPagingAsync<CarsServiceAllModel>
                    (searchString, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
                count = await this.carsService.GetCountByParkingAsync(searchString);
            }
            else
            {
                searchString = currentFilter;
            }

            this.ViewData["CurrentFilter"] = searchString;
            this.TempData["SearchString"] = searchString;

            var carsWithRatings = await this.carsService.GetAllAsync<CarsServiceAllModel>();
            viewModel.Cars = cars.Select(c => c.To<CarsAllViewModel>()).ToList();

            foreach (var car in viewModel.Cars)
            {
                car.Rating = carsWithRatings.Where(c => c.Id == car.Id)
                                            .Select(c => c.Rating)
                                            .FirstOrDefault();
            }

            viewModel.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> ReturnCar(string id)
        {
            var parkings = await this.parkingsService.GetAllAsync<ParkingsServiceDropDownModel>();

            var car = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(id);
            var viewModel = car.To<CarDetailsViewModel>();
            viewModel.Parkings = parkings.Select(p => p.To<ParkingsDropDownViewModel>()).ToList();

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ReturnCar(CarDetailsViewModel carReturnViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                var parkings = await this.parkingsService.GetAllAsync<ParkingsServiceDropDownModel>();
                carReturnViewModel.Parkings = parkings.Select(p => p.To<ParkingsDropDownViewModel>()).ToList();

                return this.View(carReturnViewModel);
            }

            var carServiceModel = carReturnViewModel.To<CarServiceDetailsModel>();
            carServiceModel.HireStatus = HireStatus.Available;
            carServiceModel.ReservationStatus = ReservationStatus.Free;

            await this.carsService.EditAsync(carServiceModel);

            return this.Redirect("/Trips/MyTrips");
        }
    }
}
