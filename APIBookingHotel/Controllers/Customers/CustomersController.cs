using AutoMapper;
using DataAccess.Entities;
using DataAccess.Models.CustomersModels;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static DataAccess.Models.Permissions;

namespace APIBookingHotel.Controllers.Customers
{

    [Route("api/Customers/")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IBookingHotelUnitOfWork _bookingHotelUnitOfWork;
        private readonly IMapper _mapper;

        public CustomersController(IBookingHotelUnitOfWork bookingHotelUnitOfWork, IMapper mapper)
        {
            _bookingHotelUnitOfWork = bookingHotelUnitOfWork;
            _mapper = mapper;
        }

        //<summary>
        //    Get all Customers from db
        //</summary>
        //
        // <param name="searchDate"></param>
        // <param name="searchString"></param>
        [HttpGet]
        [Route("GetCustomers")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCustomersAsync(DateTime? fromDate, DateTime? toDate, string? searchString)
        {
            var result = new List<CustomersViewModel>();
            var Customers = await _bookingHotelUnitOfWork.CustomerRepository.GetAll(HttpContext.RequestAborted);
            if (Customers != null)
            {
                //result = _mapper.Map<List<CustomersViewModel>>(Customers);
                return Ok(Customers);
            }
            return BadRequest();
        }

        //<summary>
        //    Get single Customers by id
        //</summary>
        //
        // <param name="id"></param>
        [HttpGet]
        [Route("SingleCustomers")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSingleCustomers(string id)
        {
            var result = new CustomersViewModel();
            var Customers = await _bookingHotelUnitOfWork.CustomerRepository.GetById(id, HttpContext.RequestAborted);
            if (Customers != null)
            {
                //result = _mapper.Map<CustomersViewModel>(Customers);
                return Ok(Customers);

            }
            return NotFound();
        }

        //<summary>
        //    Add new Customers
        //</summary>
        //
        // <param name="id"></param>
        [HttpPost]
        [Route("AddCustomers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCustomers(CustomersViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customers = new Customer();
                customers = _mapper.Map<Customer>(model);
                customers.Id = Common.Security.GenerateRandomId();
                customers.SysDate = DateTime.Now;
                var result = await _bookingHotelUnitOfWork.CustomerRepository.Add(customers, HttpContext.RequestAborted);
                if (result != null)
                {
                    await _bookingHotelUnitOfWork.SaveAsync();
                    return Ok(customers);
                }
            }
            return BadRequest();
        }

        //<summary>
        //    Edit Customers
        //</summary>
        //
        // <param name="id"></param>
        // <param name="CustomersModel"></param>
        [HttpPost]
        [Route("EditCustomers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditCustomers(string id, CustomersViewModel model)
        {
            var customers = await _bookingHotelUnitOfWork.CustomerRepository.GetById(id, HttpContext.RequestAborted);
            if (customers == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                customers = _mapper.Map<Customer>(model);
                customers.SysDate = DateTime.Now;
                var result = await _bookingHotelUnitOfWork.CustomerRepository.Update(customers, HttpContext.RequestAborted);
                if (result != null)
                {
                    return Ok(customers);
                }
            }
            return BadRequest();
        }

        //<summary>
        //    Delete Customers
        //</summary>
        //
        // <param name="id"></param>
        // <param name="CustomersModel"></param>
        [HttpDelete]
        [Route("DeleteCustomers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCustomers(string id)
        {
            var customers = await _bookingHotelUnitOfWork.CustomerRepository.GetById(id, HttpContext.RequestAborted);
            if (customers == null)
            {
                return BadRequest();
            }
            var result = await _bookingHotelUnitOfWork.CustomerRepository.Delete(id, HttpContext.RequestAborted);
            if (result != null)
            {
                return Ok(1);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AddContact")]
        [AllowAnonymous]
        public async Task<IActionResult> AddContact(CustomersViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userInfor = await _bookingHotelUnitOfWork.Identity.GetById(model.CusEmail!, HttpContext.RequestAborted);
                if (userInfor == null)
                {
                    var customerInfor = await _bookingHotelUnitOfWork.CustomerRepository.GetById(model.CusEmail!, HttpContext.RequestAborted);

                    if (customerInfor == null)
                    {
                        // tạo khách hàng để lấy CustomerId
                        model.Id = Common.Security.GenerateRandomId();
                        model.Id_Evaluate = Common.Security.GenerateRandomId();
                        var contactsResponse = _mapper.Map<CustomersViewModel>(model);
                        await _bookingHotelUnitOfWork.CustomerRepository.InsertAsync(contactsResponse, HttpContext.RequestAborted);
                        await _bookingHotelUnitOfWork.SaveAsync();
                        return Ok(1);
                    }

                    var evaluates = new Evaluate();
                    evaluates.Id = Common.Security.GenerateRandomId();
                    evaluates.CusCodeByUserId = customerInfor.Id!;
                    evaluates.Description = model.Description;
                    evaluates.CreationDate = DateTime.Now;
                    var result = await _bookingHotelUnitOfWork.EvalutesRepository.Add(evaluates, HttpContext.RequestAborted);
                    if (result != null)
                    {
                        await _bookingHotelUnitOfWork.SaveAsync();
                        return Ok(1);
                    }

                }
                else {
                    var evaluates = new Evaluate();
                    evaluates.Id = Common.Security.GenerateRandomId();
                    evaluates.UserCodeByUserId = userInfor.Id;
                    evaluates.Description = model.Description;
                    evaluates.CreationDate = DateTime.Now;
                    var result = await _bookingHotelUnitOfWork.EvalutesRepository.Add(evaluates, HttpContext.RequestAborted);
                    if (result != null)
                    {
                        await _bookingHotelUnitOfWork.SaveAsync();
                        return Ok(1);
                    }
                }

            }
            return NoContent();
        }

    }
}

