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
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Newtonsoft.Json;
using APIBookingHotel.BaseModel;
using OfficeOpenXml;
using System.Security.Claims;

namespace APIBookingHotel.Controllers.Identity
{
    [ApiController]
    [Route("api/Identity/Identity")]
    [Authorize(Roles = "Admin,User")]
    public class IdentityController : ControllerBase
    {
        private readonly IBookingHotelUnitOfWork _unitOfWork;
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private BookingHotelDbContext _db;
        private readonly IDistributedCache _cache;
        private IConfiguration _config;

        public IdentityController(BookingHotelDbContext db,IBookingHotelUnitOfWork unitOfWork,IIdentityRepository identityRepository, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IDistributedCache cache, IConfiguration configtion)
        {
            _unitOfWork = unitOfWork;
            _identityRepository = identityRepository;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _cache = cache;
            _config = configtion;
            _db = db;
        }

        /// <summary>
        /// Get All users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Users")]
        //[Authorize(Policy = Permissions.Users.View)]
        public async Task<IActionResult> GetAll(string? searchValue, string? roleValue)
        {

            var key_cache = "GET_LIST_USER_"+ roleValue;
            var cacheValue = await _cache.GetAsync(key_cache);
            if (cacheValue != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(cacheValue);
                if (cachedDataString != null)
                {

                     var list = JsonConvert.DeserializeObject<List<ProfileView>>(cachedDataString.ToString());
                    return Ok(list);
                }
            }

            var userResult = await _unitOfWork.Identity.GetAllUserInRoleAsync(searchValue!, roleValue!, HttpContext.RequestAborted);
            var result = _mapper.Map<List<ProfileView>>(userResult);

            if (result.Count > 0)
            {
                // lưu cache 
                var dataCache = JsonConvert.SerializeObject(result);

                var dataToCache = Encoding.UTF8.GetBytes(dataCache);

                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                await _cache.SetAsync(key_cache, dataToCache, options);
            }
            return Ok(result);


        }

        /// <summary>
        /// Get user detail by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUser/{id}")]
        //[Authorize(Policy = Permissions.Users.View)]
        public async Task<Result<ProfileView>> GetUser(string? id)
        {
            var userResult = await _unitOfWork.Identity.GetById(id!, HttpContext.RequestAborted);
            var usersResponse = _mapper.Map<ProfileView>(userResult);
            return await Result<ProfileView>.SuccessAsync(usersResponse);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="ProfileView"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUser")]
        //[Authorize(Policy = Permissions.Users.Edit)]
        public async Task<IActionResult> UpdateUser(ProfileView p)
        {
            var userResult = await _unitOfWork.Identity.GetById(p.Id!, HttpContext.RequestAborted);
            if (userResult == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var usersResponse = _mapper.Map<ProfileView>(p);
                await _unitOfWork.Identity.UpdateAsync(usersResponse, HttpContext.RequestAborted);
                await _unitOfWork.SaveAsync();
                return Ok(usersResponse);
            }
            return NoContent();
        }

        [HttpPost("Uploadimage")]
        public async Task<ActionResult> UploadImage(UploadImageRequestData requestData)
        {
            var returnData = new UploadImageResponseData();
            try
            {

                var url_api = _config["MEDIA:URL"] ?? "https://localhost:7125/";
                var base_url = "Upload/UploadProductImage";

                var secretKey = _config["Sercurity:secretKeyEmployeer"] ?? "CAjEbwkeGqO@#Gn3Fsd8SRs2dFLMfxTo11a";
                var Sign = Common.Security.MD5Hash(requestData.base64Image + "|" + secretKey);
                requestData.sign = Sign;

                var dataJson = JsonConvert.SerializeObject(requestData);

                //var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"].Value : string.Empty;

                var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Post,url_api, base_url, dataJson, "");

            }
            catch (Exception)
            {

                throw;
            }

            return Ok(returnData);
        }
    }
}