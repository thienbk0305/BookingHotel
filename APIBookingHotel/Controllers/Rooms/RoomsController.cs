using APIBookingHotel.LogServices;
using APIBookingHotel.Models;
using AutoMapper;
using DataAccess.Entities;
using DataAccess.Models.RoomsModels;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIBookingHotel.Controllers.rooms
{

    [Route("api/rooms/")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IBookingHotelUnitOfWork _bookingHotelUnitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;

        public RoomsController(IBookingHotelUnitOfWork bookingHotelUnitOfWork, IMapper mapper, ILoggerManager loggerManager)
        {
            _bookingHotelUnitOfWork = bookingHotelUnitOfWork;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }

        //<summary>
        //    Get all rooms from db
        //</summary>
        //
        // <param name="searchDate"></param>
        // <param name="searchString"></param>
        [HttpGet]
        [Route("GetRooms")]
        [AllowAnonymous]
        public async Task<IActionResult> GetroomsAsync(DateTime? fromDate, DateTime? toDate, string? searchString)
        {
            var result = new List<RoomsViewModel>();
            var rooms = await _bookingHotelUnitOfWork.RoomsRepository.GetAllRoomsAsync(searchString!, HttpContext.RequestAborted);
            if (rooms != null)
            {
                //result = _mapper.Map<List<roomsViewModel>>(rooms);
                _loggerManager.LogInfo("Singlerooms: " + JsonConvert.SerializeObject(rooms));
                return Ok(rooms);
            }
            return BadRequest();
        }

        //<summary>
        //    Get single rooms by id
        //</summary>
        //
        // <param name="id"></param>
        [HttpGet]
        [Route("Singlerooms")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSinglerooms(string id)
        {
            var result = new RoomsViewModel();
            var rooms = await _bookingHotelUnitOfWork.RoomsRepository.GetRoomDetailAsync(id, HttpContext.RequestAborted);
            if (rooms != null)
            {
                //result = _mapper.Map<roomsViewModel>(rooms);
                return Ok(rooms);

            }
            return NotFound();
        }

        //<summary>
        //    Add new rooms
        //</summary>
        //
        // <param name="id"></param>
        [HttpPost]
        [Route("AddRooms")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Addrooms(RoomsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var rooms = new Room();
                rooms = _mapper.Map<Room>(model);
                rooms.Id = Common.Security.GenerateRandomId();
                rooms.SysDate = DateTime.Now;
                if (!string.IsNullOrEmpty(model.ImgCode))
                {
                    var images = new Image()
                    {
                        Id = Common.Security.GenerateRandomId(),
                        ImgCode = model.ImgCode,
                        SysDate = DateTime.Now
                    };
                    var imagesResult = await _bookingHotelUnitOfWork.ImagesRepository.Add(images, HttpContext.RequestAborted);
                    rooms.ImgCodeByUserId = imagesResult.Id;
                }
                var result = await _bookingHotelUnitOfWork.RoomsRepository.Add(rooms, HttpContext.RequestAborted);
                if (result != null)
                {
                    await _bookingHotelUnitOfWork.SaveAsync();
                    return Ok(1);
                }
            }
            return BadRequest();
        }

        //<summary>
        //    Edit rooms
        //</summary>
        //
        // <param name="id"></param>
        // <param name="RoomsModel"></param>
        [HttpPost]
        [Route("Editrooms")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Editrooms(string id, RoomsViewModel model)
        {
            var rooms = await _bookingHotelUnitOfWork.RoomsRepository.GetById(id, HttpContext.RequestAborted);
            if (rooms == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                rooms = _mapper.Map<Room>(model);
                rooms.SysDate = DateTime.Now;
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
                        rooms.ImgCodeByUserId = newImagesResult.Id;
                    }

                }
                var result = await _bookingHotelUnitOfWork.RoomsRepository.Update(rooms, HttpContext.RequestAborted);
                await _bookingHotelUnitOfWork.SaveAsync();
                if (result != null)
                {
                    return Ok(1);
                }
            }
            return BadRequest();
        }

        //<summary>
        //    Delete rooms
        //</summary>
        //
        // <param name="id"></param>
        // <param name="RoomsModel"></param>
        [HttpDelete]
        [Route("DeleteRooms")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deleterooms(string id)
        {
            var rooms = await _bookingHotelUnitOfWork.RoomsRepository.GetById(id, HttpContext.RequestAborted);
            if (rooms == null)
            {
                return BadRequest();
            }
            var result = await _bookingHotelUnitOfWork.RoomsRepository.Delete(id, HttpContext.RequestAborted);
            if (result != null)
            {
                return Ok(1);
            }
            return BadRequest();
        }

    }
}