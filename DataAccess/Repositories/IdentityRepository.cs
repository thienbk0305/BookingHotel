using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Models;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class IdentityRepository : GenericRepository<User>, IIdentityRepository
    {
        private readonly BookingHotelDbContext _context;

        public IdentityRepository(BookingHotelDbContext context) : base(context)
        {
            _context = context;
        }
 
    }
}