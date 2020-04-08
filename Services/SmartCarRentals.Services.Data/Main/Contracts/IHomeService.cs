namespace SmartCarRentals.Services.Data.Main.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Services.Models.Main.Cars;

    public interface IHomeService
    {
        Task<IEnumerable<CarsHotOffersServiceModel>> GetHotOffersCarsAsync();
    }
}
