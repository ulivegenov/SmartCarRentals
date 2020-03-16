namespace SmartCarRentals.Services.Data.Administration
{
    using System.Collections.Generic;

    public interface ISettingsService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();
    }
}
