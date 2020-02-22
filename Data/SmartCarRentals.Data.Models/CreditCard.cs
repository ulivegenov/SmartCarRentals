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

       // class CreditCard
       // {
       //     public CreditCard(string number, string expiration, string cvv2) { ...}

       //     public virtual bool IsValid()
       //     {
       //         /* put common validation logic here */
       //     }

       //     /* factory for actual cards */
       //     public static CreditCard GetCardByType(CardType card, string number, string expiration, string cvv2)
       //     {
       //         switch (card)
       //         {
       //             case CardType.Visa:
       //                 return new VisaCreditCard(...);

       //                 ...
       // }
       //     }
       // }

       // class VisaCreditCard : CreditCard
       // {
       //     public VisaCreditCard(string number, string expiration, string cvv2)
       //        : base(number, expiration, cvv2)
       //     { ...}

       //     public override bool IsValid()
       //     {
       //         /* check Visa rules... */
       //         bool isValid = ...

       //return isValid & base.IsValid();
       //     }
       // }
    }
}