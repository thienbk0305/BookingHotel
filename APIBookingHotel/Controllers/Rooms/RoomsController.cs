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
            var rooms = await _bookingHotelUnitOfWork.RoomsRepository.GetAll(HttpContext.RequestAborted);
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
            var rooms = await _bookingHotelUnitOfWork.RoomsRepository.GetById(id,HttpContext.RequestAborted);
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
                var result = await _bookingHotelUnitOfWork.RoomsRepository.Add(rooms, HttpContext.RequestAborted);
                if (result  != null)
                {
                    await _bookingHotelUnitOfWork.SaveAsync();
                    return Ok(rooms);
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
            if (rooms == null) {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                rooms = _mapper.Map<Room>(model);
                rooms.SysDate = DateTime.Now;
                var result = await _bookingHotelUnitOfWork.RoomsRepository.Update(rooms, HttpContext.RequestAborted);
                if (result != null)
                {
                    return Ok(rooms);
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

