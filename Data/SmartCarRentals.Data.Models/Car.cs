﻿namespace SmartCarRentals.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models.Enums.Car;

    public class Car : BaseDeletableModel<string>
    {
        private readonly int passengersCapacity;

        public Car(string classType, string transmitionType, string fuelType, int passengersCapacity)
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;

            this.Class = this.SetType<ClassType>(classType);
            this.Transmition = this.SetType<TransmitionType>(transmitionType);
            this.Fuel = this.SetType<FuelType>(fuelType);
            this.passengersCapacity = passengersCapacity;

            this.Trips = new HashSet<Trip>();
            this.Images = new HashSet<Image>();
        }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.NameMaxLength)]
        public string Make { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.NameMaxLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.PlateNumberMaxLength)]
        public string PlateNumber { get; set; }

        public int KmRun { get; set; }

        public int ServiceKm { get; private set; }

        public int ServiceRun => this.ServiceKm - this.KmRun;

        public int PricePerHour { get; set; } // TODO Logic

        public int PricePerDay { get; set; } // TODO Logic

        public ClassType Class { get; set; }

        public ServiceStatus ServiceStatus { get; set; }

        public HireStatus HireStatus { get; set; }

        public ReservationStatus ReservationStatus { get; set; }

        public ConditionType Condition { get; set; }

        public TransmitionType Transmition { get; }

        public FuelType Fuel { get; set; }

        public int PassengersCapacity => this.passengersCapacity;

        public double Rating { get; set; }

        public int ParkingId { get; set; }

        public virtual Parking Parking { get; set; }

        public int? ParkingLotId { get; set; }

        public virtual ParkingLot ParkingLot { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        private T SetType<T>(string input)
        {
            T type = (T)Enum.Parse(typeof(T), input);

            return type;
        }
    }
}
