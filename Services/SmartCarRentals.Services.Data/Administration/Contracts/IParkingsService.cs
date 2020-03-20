﻿namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;

    public interface IParkingsService : IAdministrationService<int>
    {
        Task<IEnumerable<Parking>> GetAllByTownIdAsync(int townId);

        Task<IEnumerable<Parking>> GetAllByCountryIdAsync(int countryId);
    }
}