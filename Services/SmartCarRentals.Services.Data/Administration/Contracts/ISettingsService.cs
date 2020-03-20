namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;

    public interface ISettingsService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();
    }
}
