using AutoMapper;
using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Models;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace APIBookingHotel.Controllers.Customer
{
    [Route("api/Customer/Customer")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class CustomerController : ControllerBase
    {
        private readonly IBookingHotelUnitOfWork _unitOfWork;
        private readonly IIdentityRepository _identityRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private BookingHotelDbContext _db;
        private readonly IDistributedCache _cache;
        private IConfiguration _config;

        public CustomerController(BookingHotelDbContext db, IBookingHotelUnitOfWork unitOfWork, IIdentityRepository identityRepository, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IDistributedCache cache, IConfiguration configtion, ICustomerRepository customerRepository)
        {
            _unitOfWork = unitOfWork;
            _identityRepository = identityRepository;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _cache = cache;
            _config = configtion;
            _db = db;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Get All Customer
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //[Route("Customers")]
        ////[Authorize(Policy = Permissions.Users.View)]
        //public async Task<IActionResult> GetCustomerAll(string? searchValue)
        //{
        //    var customerResult = await _unitOfWork.CustomerRepository.GetAllCustomerAsync(searchValue!, HttpContext.RequestAborted);
        //    var result = _mapper.Map<List<ContactView>>(customerResult);
        //    return Ok(result);
        //}

         /// <summary>
         /// Add Contact
         /// </summary>
         /// <param name="ProfileView"></param>
         /// <returns></returns>
        [HttpPut]
        [Route("AddContact")]
        //[Authorize(Policy = Permissions.Users.Edit)]
        public async Task<IActionResult> AddContact(ContactView c)
        {
            if (ModelState.IsValid)
            {
                c.Id = Common.Security.GenerateRandomId();
                c.Id_Evaluate = Common.Security.GenerateRandomId();
                var contactsResponse = _mapper.Map<ContactView>(c);
                await _unitOfWork.CustomerRepository.InsertAsync(contactsResponse, HttpContext.RequestAborted);
                await _unitOfWork.SaveAsync();
                return Ok(contactsResponse);
            }
            return NoContent();
        }

    }
}
