using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Models.NewsModels;
using DataAccess.Models.SystemsModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class NewsRepository : GenericRepository<BookingHotelDbContext, New>, INewsRepository
    {
        private readonly BookingHotelDbContext _db;
        public NewsRepository(BookingHotelDbContext context) : base(context)
        {
            _db = context;
        }
        public async Task<IEnumerable<NewsViewModel>> GetAllNewsAsync(string searchValue, CancellationToken cancellation)
        {
            if (searchValue == null)
            {
                searchValue = "";
            }

            var data = await
                (from news in _db.New
                  join images in _db.Image on news.ImgCodeByUserId equals images.Id into gj
                  from subnews in gj.DefaultIfEmpty()
                 select new NewsViewModel()
                 {
                     Id = news.Id,
                     Title = news.Title,
                     SumContent = news.SumContent,
                     NewsContent = news.NewsContent,
                     Source = news.Source,
                     Active = news.Active,
                     SysDate = news.SysDate,
                     ImgCodeByUserId = news.ImgCodeByUserId,
                     ImgCode = subnews.ImgCode
                 }).ToListAsync();
            await _db.SaveChangesAsync(cancellation);
            return data!;

        }

    }
}
