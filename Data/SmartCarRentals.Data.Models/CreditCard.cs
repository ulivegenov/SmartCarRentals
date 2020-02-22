namespace SmartCarRentals.Data.Models
{
    using System;

    using SmartCarRentals.Data.Common.Models;

    public class CreditCard : BaseDeletableModel<string>
    {
        public CreditCard()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        // - Number
        // - ClientId
        // - Client


    }
}