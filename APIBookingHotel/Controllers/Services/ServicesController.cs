using AutoMapper;
using DataAccess.Entities;
using DataAccess.Models.ServicesModels;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIBookingHotel.Controllers.services
{
    
    [Route("api/services/")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IBookingHotelUnitOfWork _bookingHotelUnitOfWork;
        private readonly IMapper _mapper;

        public ServicesController(IBookingHotelUnitOfWork bookingHotelUnitOfWork, IMapper mapper)
        {
            _bookingHotelUnitOfWork = bookingHotelUnitOfWork;
            _mapper = mapper;
        }

        //<summary>
        //    Get all services from db
        //</summary>
        //
        // <param name="searchDate"></param>
        // <param name="searchString"></param>
        [HttpGet]
        [Route("GetServices")]
        [AllowAnonymous]
        public async Task<IActionResult> GetservicesAsync(DateTime? fromDate, DateTime? toDate, string? searchString)
        {
            var result = new List<ServicesViewModel>();
            var services = await _bookingHotelUnitOfWork.ServicesRepository.GetAll(HttpContext.RequestAborted);
            if (services != null)
            {
                //result = _mapper.Map<List<servicesViewModel>>(services);
                return Ok(services);
            }
            return BadRequest();
        }

        //<summary>
        //    Get single services by id
        //</summary>
        //
        // <param name="id"></param>
        [HttpGet]
        [Route("SingleServices")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSingleservices(string id)
        {
            var result = new ServicesViewModel();
            var services = await _bookingHotelUnitOfWork.ServicesRepository.GetById(id,HttpContext.RequestAborted);
            if (services != null)
            {
                //result = _mapper.Map<servicesViewModel>(services);
                return Ok(services);

            }
            return NotFound();
        }

        //<summary>
        //    Add new services
        //</summary>
        //
        // <param name="id"></param>
        [HttpPost]
        [Route("AddServices")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Addservices(ServicesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var services = new Service();
                services = _mapper.Map<Service>(model);
                services.Id = Common.Security.GenerateRandomId();
                services.SysDate = DateTime.Now;
                var result = await _bookingHotelUnitOfWork.ServicesRepository.Add(services, HttpContext.RequestAborted);
                if (result  != null)
                {
                    await _bookingHotelUnitOfWork.SaveAsync();
                    return Ok(services);
                }
            }
            return BadRequest();
        }

        //<summary>
        //    Edit services
        //</summary>
        //
        // <param name="id"></param>
        // <param name="servicesModel"></param>
        [HttpPost]
        [Route("EditServices")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Editservices(string id, ServicesViewModel model)
        {
            var services = await _bookingHotelUnitOfWork.ServicesRepository.GetById(id, HttpContext.RequestAborted);
            if (services == null) {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                services = _mapper.Map<Service>(model);
                services.SysDate = DateTime.Now;
                var result = await _bookingHotelUnitOfWork.ServicesRepository.Update(services, HttpContext.RequestAborted);
                await _bookingHotelUnitOfWork.SaveAsync();
                if (result != null)
                {
                    return Ok(services);
                }
            }
            return BadRequest();
        }

        //<summary>
        //    Delete services
        //</summary>
        //
        // <param name="id"></param>
        // <param name="servicesModel"></param>
        [HttpDelete]
        [Route("DeleteServices")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deleteservices(string id)
        {
            var services = await _bookingHotelUnitOfWork.ServicesRepository.GetById(id, HttpContext.RequestAborted);
            if (services == null)
            {
                return BadRequest();
            }
            var result = await _bookingHotelUnitOfWork.ServicesRepository.Delete(id, HttpContext.RequestAborted);
            if (result != null)
            {
                return Ok(1);
            }
            return BadRequest();
        }

    }
}

