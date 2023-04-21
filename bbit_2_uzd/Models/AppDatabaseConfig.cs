using Microsoft.EntityFrameworkCore;

namespace bbit_2_uzd.Models
{
    public class AppDatabaseConfig : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //izveido db, ja nav
            optionsBuilder.UseSqlite(@"Data Source = Apartments.db;");
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseLazyLoadingProxies(true);
        }

        public DbSet<House> Houses { get; set; } = null;
        public DbSet<Tenant> Tenants { get; set; } = null;
        public DbSet<Apartment> Apartments { get; set; } = null;

        //iedod datus ja nav, novers err 500, vienlaikus izveido migracijas celus
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            House house1 =
                new House
                {
                    Id = Guid.NewGuid(),
                    Number = 1,
                    Street = "Default",
                    City = "Test",
                    Country = "TestaValsts",
                    PostalCode = "TV-0001"
                };
            House house2 =
                new House
                {
                    Id = Guid.NewGuid(),
                    Number = 40,
                    Street = "Liela",
                    City = "Jelgava",
                    Country = "Lavia",
                    PostalCode = "LV-3001"
                };
            modelBuilder.Entity<House>(
                m =>
                {
                    m.HasKey("Id");
                    m.Property(p => p.Number).IsRequired();
                    m.Property(p => p.Street).IsRequired();
                    m.Property(p => p.City).IsRequired();
                    m.Property(p => p.Country).IsRequired();
                    m.Property(p => p.PostalCode).IsRequired();
                    m.HasIndex(p => new { p.Number, p.Street, p.City, p.Country }).IsUnique();
                    m.HasData(
                        house1,
                        house2
                    );
                }
            );
            modelBuilder.Entity<Apartment>(
                d =>
                {
                    d.HasKey(p => p.Id);
                    d.Property(p => p.Number).IsRequired();
                    d.Property(p => p.FullArea).IsRequired();
                    d.Property(p => p.LivingArea).IsRequired();
                    d.Property(p => p.HouseId).IsRequired();
                    d.HasIndex(p => new {p.Number, p.HouseId}).IsUnique();
                    d.HasData(
                            new
                            {
                                Id = Guid.NewGuid(),
                                Number = 1,
                                Floor = 1,
                                NumberOfRooms = 3,
                                NumberOfTenants = 4,
                                FullArea = 70M,
                                LivingArea = 64.5M,
                                HouseId = house1.Id
                            },
                            new
                            {
                                Id = Guid.NewGuid(),
                                Number = 2,
                                Floor = 1,
                                NumberOfRooms = 1,
                                NumberOfTenants = 0,
                                FullArea = 70M,
                                LivingArea = 64.5M,
                                HouseId = house1.Id
                            },
                            new
                            {
                                Id = Guid.NewGuid(),
                                Number = 3,
                                Floor = 2,
                                NumberOfRooms = 1,
                                NumberOfTenants = 0,
                                FullArea = 70M,
                                LivingArea = 64.5M,
                                HouseId = house1.Id
                            },
                            new
                            {
                                Id = Guid.NewGuid(),
                                Number = 4,
                                Floor = 2,
                                NumberOfRooms = 1,
                                NumberOfTenants = 0,
                                FullArea = 70M,
                                LivingArea = 64.5M,
                                HouseId = house1.Id
                            },
                            new
                            {
                                Id = Guid.NewGuid(),
                                Number = 5,
                                Floor = 3,
                                NumberOfRooms = 1,
                                NumberOfTenants = 0,
                                FullArea = 70M,
                                LivingArea = 64.5M,
                                HouseId = house1.Id
                            }
                        );
                }
                );
            modelBuilder.Entity<Tenant>(
                i =>
                {
                    i.HasKey(p => p.Id);
                    i.Property(p => p.PersonalCode).IsRequired();
                    i.Property(p => p.PhoneNumber).IsRequired();
                    i.Property(p => p.Email).IsRequired(false);
                    i.HasIndex(p => p.PersonalCode).IsUnique();
                    i.HasIndex(p => p.PhoneNumber).IsUnique();
                    i.HasIndex(p => p.Email).HasFilter("Email IS NOT NULL AND Email <> ''").IsUnique();
                    i.HasData(
                            new
                            {
                                Id = Guid.NewGuid(),
                                Name = "Janis",
                                Surname = "Tests",
                                PersonalCode = "000000-000001",
                                DateOfBirth = new DateTime(1972, 1, 1),
                                PhoneNumber = "+371 00000001",
                                Email = "janis.tests@fake.com",
                                IsOwner = true
                            },
                            new
                            {
                                Id = Guid.NewGuid(),
                                Name = "Anna",
                                Surname = "Tests",
                                PersonalCode = "000000-000002",
                                DateOfBirth = new DateTime(1974, 1, 1),
                                PhoneNumber = "+371 00000002",
                                Email = "Anna.Tests@fake.com",
                                IsOwner = false
                            },
                            new
                            {
                                Id = Guid.NewGuid(),
                                Name = "Māris",
                                Surname = "Tests",
                                PersonalCode = "000000-000003",
                                DateOfBirth = new DateTime(2000, 1, 1),
                                PhoneNumber = "+371 00000003",
                                Email = "Maris.tests@fake.com",
                                IsOwner = false
                            },
                            new
                            {
                                Id = Guid.NewGuid(),
                                Name = "Zane",
                                Surname = "Tests",
                                PersonalCode = "000000-000004",
                                DateOfBirth = new DateTime(1998, 1, 1),
                                PhoneNumber = "+371 00000004",
                                Email = "",
                                IsOwner = false
                            }
                        );
                }
                );

            modelBuilder.Entity<Apartment>()
                .HasOne(d => d.House)
                .WithMany()
                .HasForeignKey(d => d.HouseId);

            modelBuilder.Entity<Tenant>()
                .HasMany(i => i.TenantApartments)
                .WithMany();
        }
    }
}