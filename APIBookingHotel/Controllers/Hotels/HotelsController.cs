using AutoMapper;
using DataAccess.Entities;
using DataAccess.Models.HotelsModels;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIBookingHotel.Controllers.Hotels
{

    [Route("api/Hotels/")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IBookingHotelUnitOfWork _bookingHotelUnitOfWork;
        private readonly IMapper _mapper;

        public HotelsController(IBookingHotelUnitOfWork bookingHotelUnitOfWork, IMapper mapper)
        {
            _bookingHotelUnitOfWork = bookingHotelUnitOfWork;
            _mapper = mapper;
        }

        //<summary>
        //    Get all Hotels from db
        //</summary>
        //
        // <param name="searchDate"></param>
        // <param name="searchString"></param>
        [HttpGet]
        [Route("GetHotels")]
        [AllowAnonymous]
        public async Task<IActionResult> GetHotelsAsync(DateTime? fromDate, DateTime? toDate, string? searchString)
        {
            var result = new List<HotelsViewModel>();
            var Hotels = await _bookingHotelUnitOfWork.HotelsRepository.GetAllHotelsAsync(searchString!, HttpContext.RequestAborted);
            if (Hotels != null)
            {
                //result = _mapper.Map<List<HotelsViewModel>>(Hotels);
                return Ok(Hotels);
            }
            return BadRequest();
        }

        //<summary>
        //    Get single Hotels by id
        //</summary>
        //
        // <param name="id"></param>
        [HttpGet]
        [Route("SingleHotels")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSingleHotels(string id)
        {
            var result = new HotelsViewModel();
            var Hotels = await _bookingHotelUnitOfWork.HotelsRepository.GetHotelDetailAsync(id, HttpContext.RequestAborted);
            if (Hotels != null)
            {
                //result = _mapper.Map<HotelsViewModel>(Hotels);
                return Ok(Hotels);

            }
            return NotFound();
        }

        //<summary>
        //    Add new Hotels
        //</summary>
        //
        // <param name="id"></param>
        [HttpPost]
        [Route("AddHotels")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddHotels(HotelsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hotels = new Hotel();
                hotels = _mapper.Map<Hotel>(model);
                hotels.Id = Common.Security.GenerateRandomId();
                hotels.SysDate = DateTime.Now;
                if (!string.IsNullOrEmpty(model.ImgCode))
                {
                    var images = new Image()
                    {
                        Id = Common.Security.GenerateRandomId(),
                        ImgCode = model.ImgCode,
                        SysDate = DateTime.Now
                    };
                    var imagesResult = await _bookingHotelUnitOfWork.ImagesRepository.Add(images, HttpContext.RequestAborted);
                    hotels.ImgCodeByUserId = imagesResult.Id;
                }
                var result = await _bookingHotelUnitOfWork.HotelsRepository.Add(hotels, HttpContext.RequestAborted);
                if (result != null)
                {
                    await _bookingHotelUnitOfWork.SaveAsync();
                    return Ok(1);
                }
            }
            return BadRequest();
        }

        //<summary>
        //    Edit Hotels
        //</summary>
        //
        // <param name="id"></param>
        // <param name="HotelsModel"></param>
        [HttpPost]
        [Route("EditHotels")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditHotels(string id, HotelsViewModel model)
        {
            var hotels = await _bookingHotelUnitOfWork.HotelsRepository.GetById(id, HttpContext.RequestAborted);
            if (hotels == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                hotels = _mapper.Map<Hotel>(model);
                hotels.SysDate = DateTime.Now;
                if (!string.IsNullOrEmpty(model.ImgCode))
                {
                    // Neu da co imgcodebyuserid thi update
                    if (!string.IsNullOrEmpty(model.ImgCodeByUserId))
                    {
                        var imagesResult = await _bookingHotelUnitOfWork.ImagesRepository.GetById(model.ImgCodeByUserId, HttpContext.RequestAborted);
                        if (imagesResult != null)
                        {
                            imagesResult.SysDate = DateTime.Now;
                            imagesResult.ImgCode = model.ImgCode;
                            await _bookingHotelUnitOfWork.ImagesRepository.Update(imagesResult, HttpContext.RequestAborted);
                        }
                    }
                    else
                    {
                        var images = new Image()
                        {
                            Id = Common.Security.GenerateRandomId(),
                            ImgCode = model.ImgCode,
                            SysDate = DateTime.Now
                        };
                        var newImagesResult = await _bookingHotelUnitOfWork.ImagesRepository.Add(images, HttpContext.RequestAborted);
                        hotels.ImgCodeByUserId = newImagesResult.Id;
                    }

                }
                var result = await _bookingHotelUnitOfWork.HotelsRepository.Update(hotels, HttpContext.RequestAborted);
                if (result != null)
                {
                    await _bookingHotelUnitOfWork.SaveAsync();
                    return Ok(1);
                }
            }
            return BadRequest();
        }

        //<summary>
        //    Delete Hotels
        //</summary>
        //
        // <param name="id"></param>
        // <param name="HotelsModel"></param>
        [HttpDelete]
        [Route("DeleteHotels")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteHotels(string id)
        {
            var Hotels = await _bookingHotelUnitOfWork.HotelsRepository.GetById(id, HttpContext.RequestAborted);
            if (Hotels == null)
            {
                return BadRequest();
            }
            var result = await _bookingHotelUnitOfWork.HotelsRepository.Delete(id, HttpContext.RequestAborted);
            if (result != null)
            {
                return Ok(1);
            }
            return BadRequest();
        }

    }
}