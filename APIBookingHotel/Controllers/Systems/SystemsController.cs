using AutoMapper;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Models.SystemsModels;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIBookingHotel.Controllers.Systems
{

    [Route("api/Systems/")]
    [ApiController]
    public class SystemsController : ControllerBase
    {
        private readonly IBookingHotelUnitOfWork _bookingHotelUnitOfWork;
        private readonly IMapper _mapper;

        public SystemsController(IBookingHotelUnitOfWork bookingHotelUnitOfWork, IMapper mapper)
        {
            _bookingHotelUnitOfWork = bookingHotelUnitOfWork;
            _mapper = mapper;
        }

        //<summary>
        //    Get all Systems from db
        //</summary>
        //
        // <param name="searchDate"></param>
        // <param name="searchString"></param>
        [HttpGet]
        [Route("GetSystems")]
        [AllowAnonymous]

        public async Task<IActionResult> GetSystemsAsync(string? searchValue)
        {
            var systems = await _bookingHotelUnitOfWork.SystemsRepository.GetAllSystemsAsync(searchValue!,HttpContext.RequestAborted);
            var result = _mapper.Map<List<SystemsViewModel>>(systems);

            if (systems != null)
            {
                return Ok(systems);
            }
            return BadRequest();
        }
        //<summary>
        //    Get single Systems by id
        //</summary>
        //
        // <param name="id"></param>
        [HttpGet]
        [Route("SingleSystems")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSingleSystems(string id)
        {
            var result = new SystemsViewModel();
            var Systems = await _bookingHotelUnitOfWork.SystemsRepository.GetById(id, HttpContext.RequestAborted);
            if (Systems != null)
            {
                return Ok(Systems);

            }
            return NotFound();
        }

        //<summary>
        //    Add new Systems
        //</summary>
        //
        // <param name="id"></param>
        [HttpPost]
        [Route("AddSystems")]
        [AllowAnonymous]
        public async Task<IActionResult> AddSystems(HRSViewModel model)
        {
            if (ModelState.IsValid)
            {
                var systems = new HotelRoomService();
                systems = _mapper.Map<HotelRoomService>(model);
                systems.Id = Common.Security.GenerateRandomId();
                systems.SysDate = DateTime.Now;
                var result = await _bookingHotelUnitOfWork.SystemsRepository.Add(systems, HttpContext.RequestAborted);
                if (result != null)
                {
                    await _bookingHotelUnitOfWork.SaveAsync();
                    return Ok(systems);
                }
            }
            return BadRequest();
        }

        //<summary>
        //    Edit Systems
        //</summary>
        //
        // <param name="id"></param>
        // <param name="SystemsModel"></param>
        [HttpPost]
        [Route("EditSystems")]
        [AllowAnonymous]
        public async Task<IActionResult> EditSystems(string id, HRSViewModel model)
        {
             var systems = await _bookingHotelUnitOfWork.SystemsRepository.GetById(id, HttpContext.RequestAborted);
            if (systems == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var result = await _bookingHotelUnitOfWork.SystemsRepository.UpdateSystemsAsync(model, HttpContext.RequestAborted);
                if (result != null)
                {
                    return Ok(systems);
                }
            }
            return BadRequest();
        }

        //<summary>
        //    Delete Systems
        //</summary>
        //
        // <param name="id"></param>
        // <param name="SystemsModel"></param>
        [HttpDelete]
        [Route("DeleteSystems")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSystems(string id)
        {
            var systems = await _bookingHotelUnitOfWork.SystemsRepository.GetById(id, HttpContext.RequestAborted);
            if (systems == null)
            {
                return BadRequest();
            }
            var result = await _bookingHotelUnitOfWork.SystemsRepository.Delete(id, HttpContext.RequestAborted);
            if (result != null)
            {
                return Ok(1);
            }
            return BadRequest();
        }

    }
}


