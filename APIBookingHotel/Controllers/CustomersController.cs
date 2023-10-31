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

    }
}
