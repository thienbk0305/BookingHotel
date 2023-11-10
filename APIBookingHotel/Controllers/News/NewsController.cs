using AutoMapper;
using DataAccess.Entities;
using DataAccess.Models.NewsModels;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIBookingHotel.Controllers.News
{
    [Authorize(Roles = "Admin")]
    [Route("api/news/")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IBookingHotelUnitOfWork _bookingHotelUnitOfWork;
        private readonly IMapper _mapper;

        public NewsController(IBookingHotelUnitOfWork bookingHotelUnitOfWork, IMapper mapper)
        {
            _bookingHotelUnitOfWork = bookingHotelUnitOfWork;
            _mapper = mapper;
        }

        //<summary>
        //    Get all news from db
        //</summary>
        //
        // <param name="searchDate"></param>
        // <param name="searchString"></param>
        [HttpGet]
        [Route("getnews")]
        public async Task<IActionResult> GetNewsAsync(DateTime? fromDate, DateTime? toDate, string? searchString)
        {
            var result = new List<NewsViewModel>();
            var news = await _bookingHotelUnitOfWork.NewsRepository.GetAll(HttpContext.RequestAborted);
            if (news != null)
            {
                //result = _mapper.Map<List<NewsViewModel>>(news);
                return Ok(news);
            }
            return BadRequest();
        }

        //<summary>
        //    Get single news by id
        //</summary>
        //
        // <param name="id"></param>
        [HttpGet]
        [Route("singlenews")]
        public async Task<IActionResult> GetSingleNews(string id)
        {
            var result = new NewsViewModel();
            var news = await _bookingHotelUnitOfWork.NewsRepository.GetById(id,HttpContext.RequestAborted);
            if (news != null)
            {
                //result = _mapper.Map<NewsViewModel>(news);
                return Ok(news);

            }
            return NotFound();
        }

        //<summary>
        //    Add new news
        //</summary>
        //
        // <param name="id"></param>
        [HttpPut]
        [Route("addnews")]
        public async Task<IActionResult> AddNews(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var news = new New();
                news = _mapper.Map<New>(model);
                news.Id = Common.Security.GenerateRandomId();
                news.Datetime = DateTime.Now;
                var result = await _bookingHotelUnitOfWork.NewsRepository.Add(news, HttpContext.RequestAborted);
                if (result  != null)
                {
                    await _bookingHotelUnitOfWork.SaveAsync();
                    return NoContent();
                }
            }
            return BadRequest();
        }

        //<summary>
        //    Edit news
        //</summary>
        //
        // <param name="id"></param>
        // <param name="NewsModel"></param>
        [HttpPost]
        [Route("editnews")]
        public async Task<IActionResult> EditNews(string id,NewsViewModel model)
        {
            var news = await _bookingHotelUnitOfWork.NewsRepository.GetById(id, HttpContext.RequestAborted);
            if (news == null) {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                news = _mapper.Map<New>(model);
                var result = await _bookingHotelUnitOfWork.NewsRepository.Update(news, HttpContext.RequestAborted);
                if (result != null)
                {
                    
                    return NoContent();
                }
            }
            return BadRequest();
        }

        //<summary>
        //    Delete news
        //</summary>
        //
        // <param name="id"></param>
        // <param name="NewsModel"></param>
        [HttpDelete]
        [Route("deletenews")]
        public async Task<IActionResult> DeleteNews(string id)
        {
            var news = await _bookingHotelUnitOfWork.NewsRepository.GetById(id, HttpContext.RequestAborted);
            if (news == null)
            {
                return BadRequest();
            }
            var result = await _bookingHotelUnitOfWork.NewsRepository.Delete(id, HttpContext.RequestAborted);
            if (result != null)
            {
                
                return NoContent();
            }
            return BadRequest();
        }

    }
}

