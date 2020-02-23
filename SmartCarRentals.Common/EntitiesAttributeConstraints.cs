﻿namespace SmartCarRentals.Common
{
    public class EntitiesAttributeConstraints
    {
        // Common
        public const int NameMinLength = 2;
        public const int NameMaxLength = 50;

        public const int AddressMaxLength = 250;

        // Users
        public const int UsernameMaxLength = 200;
        public const int EmailMinLength = 6;
        public const int EmailMaxLength = 250;

        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 100;

        public const int AgeMin = 18;
        public const int AgeMax = 100;

        // Car
        public const int DefaultServiceRun = 10000;

        public const int PlateNumberMinLength = 4;
        public const int PlateNumberMaxLength = 30;

        public const int MinPassengers = 1;
        public const int MaxPassengers = 12;

        // Image
        public const int UrlMaxLength = 250;

        // Parking
        public const int MinCapacity = 1;
        public const int MaxCapacity = 10000;

        // Rating
        public const int MinRatingVote = 1;
        public const int MaxRatingVote = 10;

        public const int CommentMaxLength = 250;
    }
}
