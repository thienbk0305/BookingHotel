using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DBContext
{
    public class BookingHotelDbContext : IdentityDbContext
    {
        public BookingHotelDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Hotel>()
            .HasMany(h => h.Service) // Assuming 'Service' is the collection navigation property in 'Hotel'
            .WithOne(s => s.HotelCodeByUser) // Assuming 'Hotel' is the navigation property in 'Service' pointing back to 'Hotel'
            .HasForeignKey(s => s.HotelCodeByUserId); // Assuming 'HotelId' is the foreign key property in 'Service'

            //builder.Entity<Hotel>()
            //.HasMany(h => h.Room) // Assuming 'Room' is the collection navigation property in 'Hotel'
            //.WithOne(s => s.HotelCodeByUser) // Assuming 'Hotel' is the navigation property in 'Room' pointing back to 'Hotel'
            //.HasForeignKey(s => s.HotelCodeByUserId); // Assuming 'HotelId' is the foreign key property in 'Room'

             builder.Entity<Room>()
            .HasMany(h => h.Service) // Assuming 'Service' is the collection navigation property in 'Hotel'
            .WithOne(s => s.RoomCodeByUser) // Assuming 'Hotel' is the navigation property in 'Service' pointing back to 'Hotel'
            .HasForeignKey(s => s.RoomCodeByUserId); // Assuming 'HotelId' is the foreign key property in 'Service'

        }

        public virtual DbSet<Booking> Booking { get; set; } = null!;
        public virtual DbSet<Customer> Customer { get; set; } = null!;
        public virtual DbSet<Evaluate> Evaluate { get; set; } = null!;
        public virtual DbSet<Hotel> Hotel { get; set; } = null!;
        public virtual DbSet<Image> Image { get; set; } = null!;
        public virtual DbSet<Menu> Menu { get; set; } = null!;
        public virtual DbSet<New> New { get; set; } = null!;
        public virtual DbSet<Room> Room { get; set; } = null!;
        public virtual DbSet<SaleOff> SaleOff { get; set; } = null!;
        public virtual DbSet<Service> Service { get; set; } = null!;
        public virtual DbSet<User> User { get; set; } = null!;

    }
}
