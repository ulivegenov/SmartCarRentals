namespace SmartCarRentals.Data.Models
{
    using System;

    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models.Enums.Tire;

    public class Tire : BaseDeletableModel<int>
    {
        public Tire(string productionDate, string season, string carId)
        {
            this.ProductionDate = this.GetDate(productionDate);
            this.Season = this.GetSeason(season);
            this.UsingStatus = this.GetUsingStatus(DateTime.UtcNow, season);
            this.ConditionStatus = ConditionStatus.Ok;
            this.CarId = carId;
        }

        public DateTime ProductionDate { get; set; }

        public Season Season { get; set; }

        public int Age { get; set; }

        public int KmRun { get; set; }

        public UsingStatus UsingStatus { get; set; }

        public ConditionStatus ConditionStatus { get; set; }

        public string CarId { get; set; }

        public virtual Car Car { get; set; }

        private DateTime GetDate(string productionDate)
        {
            DateTime date = DateTime.Parse(productionDate);

            return date;
        }

        private Season GetSeason(string season)
        {
            Season seasonType = Enum.Parse<Season>(season);

            return seasonType;
        }

        private UsingStatus GetUsingStatus(DateTime utcNow, string season)
        {
            bool isSummer = utcNow.Month == 5 ||
                            utcNow.Month == 6 ||
                            utcNow.Month == 7 ||
                            utcNow.Month == 8 ||
                            utcNow.Month == 9 ||
                            utcNow.Month == 10;

            if ((isSummer && this.GetSeason(season) == Season.Summer) ||
                (!isSummer && this.GetSeason(season) == Season.Winter))
            {
                return UsingStatus.InUse;
            }

            return UsingStatus.InHotel;
        }
    }
}
