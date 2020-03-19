namespace SmartCarRentals.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDriversService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
