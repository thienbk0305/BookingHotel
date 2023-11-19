using AutoMapper;
using DataAccess.Entities;
using DataAccess.Models.NewsModels;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIBookingHotel.Controllers.News
{
    
    [Route("api/News/")]
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
        [Route("GetNews")]
        [AllowAnonymous]
        //public async Task<IActionResult> GetNewsAsync(DateTime? fromDate, DateTime? toDate, string? searchString)
        //{
        //    var result = new List<NewsViewModel>();
        //    var news = await _bookingHotelUnitOfWork.NewsRepository.GetAll(HttpContext.RequestAborted);
        //    if (news != null)
        //    {
        //        //result = _mapper.Map<List<NewsViewModel>>(news);
        //        return Ok(news);
        //    }
        //    return BadRequest();
        //}

        public async Task<IActionResult> GetNewsAsync(DateTime? fromDate, DateTime? toDate, string? searchString)
        {
            string sql = "Select N.*,I.FileName From New N Left Join Image I On N.ImgCodeByUserId=I.Id";
            var result = new List<NewsViewModel>();
            var news = await _bookingHotelUnitOfWork.NewsRepository.FromSqlQueryAsync(sql, HttpContext.RequestAborted);
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
        [Route("SingleNews")]
        [AllowAnonymous]
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
        [HttpPost]
        [Route("AddNews")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNews(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var news = new New();
                news = _mapper.Map<New>(model);
                news.Id = Common.Security.GenerateRandomId();
                news.SysDate = DateTime.Now;
                if (model.ImgCodeByUserId != null)
                {
                    var images = new Image()
                    {
                        Id = Common.Security.GenerateRandomId(),
                        ImgCode = model.ImgCodeByUserId,
                        SysDate = DateTime.Now
                    };
                    var imagesResult = await _bookingHotelUnitOfWork.ImagesRepository.Add(images, HttpContext.RequestAborted);
                    news.ImgCodeByUserId = imagesResult.Id;
                }    
                    var result = await _bookingHotelUnitOfWork.NewsRepository.Add(news, HttpContext.RequestAborted);
                if (result  != null)
                {
                    await _bookingHotelUnitOfWork.SaveAsync();
                    return Ok(news);
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
        [Route("EditNews")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditNews(string id, NewsViewModel model)
        {
            var news = await _bookingHotelUnitOfWork.NewsRepository.GetById(id, HttpContext.RequestAborted);
            if (news == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                news = _mapper.Map<New>(model);
                news.SysDate = DateTime.Now;
                if (model.ImgCodeByUserId != null)
                {

                    var imagesResult = await _bookingHotelUnitOfWork.ImagesRepository.GetById(id, HttpContext.RequestAborted);
                    if (imagesResult != null)
                    {
                        imagesResult.SysDate = DateTime.Now;
                        await _bookingHotelUnitOfWork.ImagesRepository.Update(imagesResult, HttpContext.RequestAborted);
                    }
                    else
                    {
                        var images = new Image()
                        {
                            Id = Common.Security.GenerateRandomId(),
                            ImgCode = model.ImgCodeByUserId,
                            SysDate = DateTime.Now
                        };
                        imagesResult= await _bookingHotelUnitOfWork.ImagesRepository.Add(images, HttpContext.RequestAborted);
                    }
                    news.ImgCodeByUserId = imagesResult.Id;
                }

                var result = await _bookingHotelUnitOfWork.NewsRepository.Update(news, HttpContext.RequestAborted);
                if (result != null)
                {
                    await _bookingHotelUnitOfWork.SaveAsync();
                    return Ok(news);
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
        [Route("DeleteNews")]
        [Authorize(Roles = "Admin")]
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
                return Ok(1);
            }
            return BadRequest();
        }

    }
}

