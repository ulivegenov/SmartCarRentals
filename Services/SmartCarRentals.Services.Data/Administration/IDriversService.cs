namespace SmartCarRentals.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Services.Models.Drivers;

    public interface IDriversService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> CreateAsync(DriverServiceInputModel driverServiceModel);
    }
}
