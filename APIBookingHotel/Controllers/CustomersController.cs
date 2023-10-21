using DataAccess.Entities;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBookingHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private IBookingHotelUnitOfWork _unitOfWork;
        public CustomersController (IBookingHotelUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            return Ok(customers);
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            _unitOfWork.CustomerRepository.Add(customer);
            await _unitOfWork.SaveAsync();
            return Ok(201);
        }
        //[HttpDelete("Delete/{id}")]
        //public ActionResult DeleteCustomer(Customer customer)
        //{
        //    _unitOfWork.CustomerRepository.Delete(customer);
        //    _unitOfWork.SaveAsync();
        //    return Ok();
        //}
    }
}
