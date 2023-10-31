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

namespace APIBookingHotel.Controllers.Identity
{
    [ApiController]
    [Route("api/Identity/Role")]
    [Authorize(Roles = "Admin")]
    public class IdentityController : ControllerBase
    {
        private readonly IBookingHotelUnitOfWork _unitOfWork;
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityController(IBookingHotelUnitOfWork unitOfWork,IIdentityRepository identityRepository, IMapper mapper,UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _identityRepository = identityRepository;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
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
            IEnumerable<User> userResult = new List<User>();
            
            if (string.IsNullOrEmpty(roleValue)) {
                if (string.IsNullOrEmpty(searchValue))
                {
                    userResult = await _unitOfWork.Identity.GetAllAsync();
                } else
                {
                    userResult = await _unitOfWork.Identity.GetManyAsync(d => d.FullName!.ToLower().Contains(searchValue!.ToLower()) || d.UserName.ToLower().Contains(searchValue.ToLower()));
                }

            } else
            {
                if (string.IsNullOrEmpty(searchValue))
                {
                    userResult = await _userManager.GetUsersInRoleAsync(roleValue);
                    //users = await _unitOfWork.Identity.GetAllAsync();
                }
                else
                {
                    var roleResult = await _userManager.GetUsersInRoleAsync(roleValue);
                    userResult = roleResult.Where(d => d.FullName!.ToLower().Contains(searchValue!.ToLower()) || d.UserName.ToLower().Contains(searchValue.ToLower())).ToList();

                }
            }
            var result = _mapper.Map<IEnumerable<ProfileView>>(userResult);
            return Ok(result);
        }

        //    /// <summary>
        //    /// Get All active users, sort by Username
        //    /// </summary>
        //    /// <returns></returns>
        //    [HttpGet]
        //    [Authorize(Policy = Permissions.User.View)]
        //    [Route("activeusers")]
        //    public async Task<IActionResult> GetAllActiveUsers(int pageNumber = 1, int pageSize = 10)
        //    {
        //        var users = await _repository.GetAllActiveUser(pageNumber,pageSize);
        //        var result = _mapper.Map<PaginatedResult<UserDto>>(users);
        //        return Ok(result);
        //    }

        //    /// <summary>
        //    /// Get user by department, sort by Username
        //    /// </summary>
        //    /// <returns></returns>
        //    [HttpGet]
        //    [Authorize(Policy = Permissions.User.View)]
        //    [Route("users/{departmentId}")]
        //    public async Task<IActionResult> GetUsersByDepartment(byte departmentId, int pageNumber = 1, int pageSize = 10)
        //    {
        //        var users = await _repository.GetUserByDepartment(departmentId, pageNumber, pageSize);
        //        var result = _mapper.Map<PaginatedResult<UserDto>>(users);
        //        return Ok(result);
        //    }

        //    /// <summary>
        //    /// Get user detail by Id
        //    /// </summary>
        //    /// <param name="id"></param>
        //    /// <returns></returns>
        //    [HttpGet]
        //    [Authorize(Policy = Permissions.User.View)]
        //    [Route("user/{id}")]
        //    public async Task<IActionResult> GetUser(string id)
        //    {
        //        var user = await _repository.GetById(id);
        //        if (user == null)
        //        {
        //            return NotFound();
        //        }
        //        var result = _mapper.Map<UserDto>(user);
        //        return Ok(result);
        //    }
        //    /// <summary>
        //    /// Create new user
        //    /// </summary>
        //    /// <param name="model"></param>
        //    /// <returns></returns>
        //    [HttpPost]
        //    [Route("user")]
        //    [Authorize(Policy = Permissions.User.Create)]
        //    public async Task<IActionResult> Post(AddEditUserDto model)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var newUser = _mapper.Map<User>(model);
        //            newUser.Active = true;
        //            IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);
        //            if (result.Succeeded)
        //            {
        //                IdentityRole role = await _roleManager.FindByIdAsync(model.RoleId);
        //                if (role != null)
        //                {
        //                    IdentityResult roleResult = await _userManager.AddToRoleAsync(newUser, role.Name);
        //                    if (roleResult.Succeeded)
        //                    {
        //                        return CreatedAtAction(nameof(GetUser),new {Id = newUser.Id}, newUser);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                return BadRequest();
        //            }
        //        }
        //        return BadRequest();
        //    }

        //    /// <summary>
        //    /// Get roles of user 
        //    /// </summary>
        //    /// <param name="userId"></param>
        //    /// <returns></returns>
        //    [HttpGet]
        //    [Authorize]
        //    [Route("roles/{userId}")]
        //    public async Task<IResult<UserRolesResponse>> GetRolesAsync(string userId)
        //    {
        //        var viewModel = new List<UserRoleModel>();
        //        var user = await _userManager.FindByIdAsync(userId);
        //        var roles = await _roleManager.Roles.ToListAsync();

        //        foreach (var role in roles)
        //        {
        //            var userRolesViewModel = new UserRoleModel
        //            {
        //                RoleName = role.Name,
        //            };
        //            if (await _userManager.IsInRoleAsync(user, role.Name))
        //            {
        //                userRolesViewModel.Selected = true;
        //            }
        //            else
        //            {
        //                userRolesViewModel.Selected = false;
        //            }
        //            viewModel.Add(userRolesViewModel);
        //        }
        //        var result = new UserRolesResponse { UserRoles = viewModel };
        //        return await Result<UserRolesResponse>.SuccessAsync(result);
        //    }

        //    /// <summary>
        //    /// Update roles of user
        //    /// </summary>
        //    /// <param name="request"></param>
        //    /// <returns></returns>

        //    [HttpPut]
        //    [Route("role")]
        //    [Authorize(Policy = Permissions.User.Edit)]
        //    public async Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request)
        //    {
        //        var user = await _userManager.FindByIdAsync(request.UserId);
        //        var roles = await _userManager.GetRolesAsync(user);
        //        var selectedRoles = request.UserRoles.Where(x => x.Selected).ToList();
        //        var result = await _userManager.RemoveFromRolesAsync(user, roles);
        //        result = await _userManager.AddToRolesAsync(user, selectedRoles.Select(y => y.RoleName));
        //        return await Result.SuccessAsync("Roles Updated");
        //    }


        //    /// <summary>
        //    /// Reset password for user
        //    /// </summary>
        //    /// <param name="request"></param>
        //    /// <returns></returns>
        //    [HttpPost]
        //    [Authorize(Policy = Permissions.User.Edit)]
        //    [Route("resetpassword")]
        //    public async Task<IResult> ResetPasswordAsync(ResetPasswordRequest request)
        //    {
        //        var user = await _userManager.FindByIdAsync(request.Id);
        //        if (user == null)
        //        {
        //            // Don't reveal that the user does not exist
        //            return await Result.FailAsync("An Error has occured!");
        //        }

        //        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
        //        if (result.Succeeded)
        //        {
        //            return await Result.SuccessAsync("Password Reset Successful!");
        //        }
        //        else
        //        {
        //            return await Result.FailAsync("An Error has occured!");
        //        }
        //    }

        //    /// <summary>
        //    /// Change account's password
        //    /// </summary>
        //    /// <param name="model"></param>
        //    /// <param name="userId"></param>
        //    /// <returns></returns>

        //    [HttpPost]
        //    [Authorize]
        //    [Route("changepassword")]

        //    public async Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, string userId)
        //    {
        //        var user = await this._userManager.FindByIdAsync(userId);
        //        if (user == null)
        //        {
        //            return await Result.FailAsync("User Not Found.");
        //        }

        //        var identityResult = await this._userManager.ChangePasswordAsync(
        //            user,
        //            model.Password,
        //            model.NewPassword);
        //        var errors = identityResult.Errors.Select(e => e.Description.ToString()).ToList();
        //        return identityResult.Succeeded ? await Result.SuccessAsync() : await Result.FailAsync(errors);
        //    }

    }
}