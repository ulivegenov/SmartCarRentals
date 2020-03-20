namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<int> GetCountAsync();
    }
}
