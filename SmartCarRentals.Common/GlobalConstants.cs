namespace SmartCarRentals.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "SmartCarRentals";

        // Roles constants
        public const string AdministratorRoleName = "Administrator";
        public const string UserRoleName = "User";

        // Cloudinary constants
        public const string CarsImagesFolder = "SmartCarRentalsPics/Cars";
        public const string DriversImagesFolder = "SmartCarRentalPics/Drivers";

        // Points Constants
        public const int GoldUserMinPoints = 200;
        public const int PlatinumUserMinPoints = 500;

        // Discounts Constants in percent
        public const int UserDiscount = 0;
        public const int GoldUserDiscount = 5;
        public const int PlatinumUserDiscount = 10;

        // Emai
        public const string ApplicationEmail = "ulivegenov@students.softuni.bg";

        // Controllers constants
        public const int ItemsPerPage = 6;
        public const int ItemsPerPageAdmin = 5;
        public const int UsersPerPageAdmin = 10;
        public const int MorePagesToShow = 2;

    }
}
