namespace Oversteer.Data
{
    using System.Linq;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using static Oversteer.Data.Common.Constants.DataConstants;
    using Oversteer.Data.Models.Cars;
    using Oversteer.Data.Models.Others;
    using Oversteer.Data.Models.Rentals;
    using Oversteer.Data.Models.Users;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<CompanyService> CompanyServices { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<CarBrand> CarBrands { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarImage> CarImages { get; set; }

        public DbSet<CarType> CarTypes { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Fuel> Fuels { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public DbSet<Transmission> Transmissions { get; set; }

        public DbSet<CarModel> CarModels { get; set; }

        public DbSet<CarFeature> CarFeatures { get; set; }

        public DbSet<CarRentDays> CarRentDays { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasOne(x => x.Country)
                    .WithMany(x => x.ApplicationUsers)
                    .HasForeignKey(x => x.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.City)
                    .WithMany(x => x.Users)
                    .HasForeignKey(x => x.CityId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Country>(entity =>
            {
                entity.HasMany(x => x.Cities)
                    .WithOne(x => x.Country)
                    .HasForeignKey(x => x.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Car>(entity =>
            {
                entity.HasOne(x => x.Fuel)
                    .WithMany(x => x.Cars)
                    .HasForeignKey(x => x.FuelId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Color)
                    .WithMany(x => x.Cars)
                    .HasForeignKey(x => x.ColorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Transmission)
                    .WithMany(x => x.Cars)
                    .HasForeignKey(x => x.TransmissionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Brand)
                    .WithMany(x => x.Cars)
                    .HasForeignKey(x => x.CarBrandId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Model)
                    .WithMany(x => x.Cars)
                    .HasForeignKey(x => x.CarModelId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Company)
                     .WithMany(x => x.Cars)
                     .HasForeignKey(x => x.CompanyId)
                     .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.DailyPrice)
                    .HasColumnType(SpecifyDecimalColumnType);
            });

            builder.Entity<Company>(entity =>
            {
                entity.HasOne(x => x.User)
                        .WithOne(x => x.Company)
                        .HasForeignKey<Company>(x => x.UserId)
                        .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Client>(entity =>
            {
                entity.HasOne(x => x.User)
                    .WithOne(x => x.Client)
                    .HasForeignKey<Client>(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            var entityTypes = builder.Model.GetEntityTypes().ToList();
            var foreignKeys = entityTypes
                    .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
        }
    }
}
