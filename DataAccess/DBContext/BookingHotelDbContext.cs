using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DBContext
{
    public class BookingHotelDbContext : IdentityDbContext
    {
        public BookingHotelDbContext (DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder) { base.OnModelCreating(builder); }

        public virtual DbSet<Booking> Booking { get; set; } = null!;
        public virtual DbSet<Customer> Customer { get; set; } = null!;
        public virtual DbSet<Evaluate> Evaluate { get; set; } = null!;
        public virtual DbSet<Hotel> Hotel { get; set; } = null!;
        public virtual DbSet<Image> Image { get; set; } = null!;
        public virtual DbSet<Language> Language { get; set; } = null!;
        public virtual DbSet<Menu> Menu { get; set; } = null!;
        public virtual DbSet<New> New { get; set; } = null!;
        public virtual DbSet<Policy> Policy { get; set; } = null!;
        public virtual DbSet<Room> Room { get; set; } = null!;
        public virtual DbSet<SaleOff> SaleOff { get; set; } = null!;
        public virtual DbSet<Service> Service { get; set; } = null!;
        public virtual DbSet<User> User { get; set; } = null!;

    }
}
