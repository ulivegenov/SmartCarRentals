namespace SmartCarRentals.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Parking> Parkings { get; set; }

        public DbSet<ParkingSlot> ParkingSlots { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Transfer> Transfers { get; set; }

        public DbSet<TransferType> TransfersTypes { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<CarRating> CarsRatings { get; set; }

        public DbSet<DriverRating> DriversRatings { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void ConfigureUserIdentityRelations(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-many realtionship between User and Car
            builder.Entity<CarRating>()
                .HasKey(cr => new { cr.CarId, cr.ClientId, cr.TripId });

            builder.Entity<CarRating>()
                .HasOne(cr => cr.Car)
                .WithMany(c => c.Ratings)
                .HasForeignKey(cr => cr.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CarRating>()
                .HasOne(cr => cr.Client)
                .WithMany(cl => cl.CarRatings)
                .HasForeignKey(cr => cr.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-one relationship between Trip and CarRating
            //builder.Entity<CarRating>()
            //    .HasOne(cr => cr.Trip)
            //    .WithOne(t => t.CarRating)
            //    .HasForeignKey<CarRating>(cr => cr.TripId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // Many-to-many relationship between User and Driver
            builder.Entity<DriverRating>()
                .HasKey(dr => new { dr.DriverId, dr.ClientId, dr.TransferId });

            builder.Entity<DriverRating>()
                .HasOne(dr => dr.Driver)
                .WithMany(cl => cl.Ratings)
                .HasForeignKey(dr => dr.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DriverRating>()
                .HasOne(dr => dr.Client)
                .WithMany(cl => cl.DriverRatings)
                .HasForeignKey(dr => dr.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to one relationship between Transfer and DriverRating
            //builder.Entity<DriverRating>()
            //    .HasOne(dr => dr.Transfer)
            //    .WithOne(t => t.DriverRating)
            //    .HasForeignKey<DriverRating>(dr => dr.TransferId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
