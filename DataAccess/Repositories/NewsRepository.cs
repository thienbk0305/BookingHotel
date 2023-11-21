using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Models.NewsModels;
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
        public NewsRepository(BookingHotelDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<NewsViewModel>> GetAllNewsAsync(string searchValue, CancellationToken cancellation)
        {
            if (searchValue == null)
            {
                searchValue = "";
            }

            var data = await
                (from news in _context.New
                 join images in _context.Image on news.ImgCodeByUserId equals images.Id into gj
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
            await _context.SaveChangesAsync(cancellation);
            return data!;

        }

        public async Task<NewsViewModel> GetNewsDetail(string id, CancellationToken cancellation)
        {
            var data = await
                (from news in _context.New
                 join images in _context.Image on news.ImgCodeByUserId equals images.Id into gj
                 from subnews in gj.DefaultIfEmpty()
                 where news.Id == id
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
                 }).FirstOrDefaultAsync();
            await _context.SaveChangesAsync(cancellation);
            return data!;
        }
    }
}