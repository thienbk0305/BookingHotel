using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.IRepositories;
using DataAccess.UnitOfWork;
using APIBookingHotel.Models;
using Microsoft.AspNetCore.Rewrite;
using DataAccess.DBContext;
using static DataAccess.Models.Permissions;
using DataAccess.Repositories;
using System.Data;

namespace APIBookingHotel.Controllers.Identity
{
    [ApiController]
    [Route("api/Identity/Identity")]
    [Authorize(Roles = "Admin")]
    public class IdentityController : ControllerBase
    {
        private readonly IBookingHotelUnitOfWork _unitOfWork;
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private BookingHotelDbContext _db;

        public IdentityController(BookingHotelDbContext db,IBookingHotelUnitOfWork unitOfWork,IIdentityRepository identityRepository, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _identityRepository = identityRepository;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        /// <summary>
        /// Get All users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Policy = Permissions.User.View)]
        [Route("Users")]
        public async Task<IActionResult> GetAll(string? searchValue, string? roleValue)
        {
            var userResult = await _unitOfWork.Identity.GetAllUserInRoleAsync(searchValue!,roleValue!, HttpContext.RequestAborted);

            var result = _mapper.Map<List<ProfileView>>(userResult);
            return Ok(result);
        }

        /// <summary>
        /// Get user detail by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Policy = Permissions.User.View)]
        [Route("GetUser/{id}")]
        public async Task<Result<ProfileView>> GetUser(string? id)
        {
            var userResult = await _unitOfWork.Identity.GetById(id!, HttpContext.RequestAborted);
            var usersResponse = _mapper.Map<ProfileView>(userResult);
            return await Result<ProfileView>.SuccessAsync(usersResponse);
        }

    }
}