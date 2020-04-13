namespace SmartCarRentals.Common
{
    public class EntitiesAttributeConstraints
    {
        // Common
        public const int NameMinLength = 2;
        public const int NameMaxLength = 50;

        public const int AddressMaxLength = 250;
        public const int AddressMinLength = 2;

        // Users
        public const int UsernameMaxLength = 200;
        public const int EmailMinLength = 6;
        public const int EmailMaxLength = 250;

        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 100;

        public const int AgeMin = 18;
        public const int AgeMax = 100;

        // Car
        public const int MinPrice = 1;
        public const int MaxPrice = 200000;

        public const int PlateNumberMinLength = 4;
        public const int PlateNumberMaxLength = 30;

        public const int MinPassengers = 1;
        public const int MaxPassengers = 12;

        // Image
        public const int UrlMinLength = 5;
        public const int UrlMaxLength = 250;

        // Parking
        public const int MinCapacity = 1;
        public const int MaxCapacity = 10000;

        // Rating
        public const int MinRatingVote = 1;
        public const int MaxRatingVote = 10;

        // Comment
        public const int CommentMaxLength = 250;

        // Message
        public const int MessageMinLength = 10;
        public const int MessageMaxLength = 5000;

        // Subject
        public const int SubjectMinLength = 2;
        public const int SubjectMaxLength = 50;

        // Date
        public const string InvalidDateMessage = "Inavlid date!";
    }
}
