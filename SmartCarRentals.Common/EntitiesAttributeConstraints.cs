namespace SmartCarRentals.Common
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
    }
}
