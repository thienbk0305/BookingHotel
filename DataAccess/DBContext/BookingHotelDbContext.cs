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

            // Configure HotelRoomService as a junction table
            builder.Entity<HotelRoomService>()
                .HasKey(hrs => new { hrs.HotelId, hrs.RoomId, hrs.ServiceId });

            builder.Entity<HotelRoomService>()
                .HasOne(hrs => hrs.Hotel)
                .WithMany(h => h.HotelRoomService)
                .HasForeignKey(hrs => hrs.HotelId);

            builder.Entity<HotelRoomService>()
                .HasOne(hrs => hrs.Room)
                .WithMany(r => r.HotelRoomService)
                .HasForeignKey(hrs => hrs.RoomId);

            builder.Entity<HotelRoomService>()
                .HasOne(hrs => hrs.Service)
                .WithMany(s => s.HotelRoomService)
                .HasForeignKey(hrs => hrs.ServiceId);

            // Additional configurations for other relationships or constraints
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
        public virtual DbSet<HotelRoomService> HotelRoomService { get; set; }

    }
}
