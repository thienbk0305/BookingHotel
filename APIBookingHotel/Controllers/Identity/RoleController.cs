using System.Reflection.Metadata.Ecma335;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Common;
using static DataAccess.Models.Permissions;

namespace APIBookingHotel.Controllers.Identity
{
    [ApiController]
    [Route("api/Identity/Role")]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Policy = Permissions.User.View)]
        [Route("Roles")]
        public async Task<IActionResult> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var rolesResponse = _mapper.Map<List<RoleResponse>>(roles);
            var result = await Result<List<RoleResponse>>.SuccessAsync(rolesResponse);
            return Ok(result);
        }

        /// <summary>
        /// Get role detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        //[Authorize(Policy = Permissions.User.Edit)]
        [Route("GetRole/{id}")]
        public async Task<Result<RoleResponse>> GetByIdAsync(string id)
        {
            var roles = await _roleManager.Roles.SingleOrDefaultAsync(x => x.Id == id);
            var rolesResponse = _mapper.Map<RoleResponse>(roles);
            return await Result<RoleResponse>.SuccessAsync(rolesResponse);
        }

        /// <summary>
        /// Create or update role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost]
        //[Authorize(Policy = Permissions.User.Edit)]
        [Route("Save")]
        public async Task<Result<string>> SaveAsync(RoleResponse request)
        {   //Add New Role
            if (string.IsNullOrEmpty(request.Id))
            {
                var existingRole = await _roleManager.FindByNameAsync(request.Name);
                if (existingRole != null)
                    return await Result<string>.FailAsync("Role này đã tồn tại");

                var response = await _roleManager.CreateAsync(new IdentityRole(request.Name));
                if (response.Succeeded)
                {
                    return await Result<string>.SuccessAsync(string.Format("Role {0} Created.", request.Name));
                }
                else
                {
                    return await Result<string>.FailAsync(response.Errors.Select(e => e.Description.ToString()).ToList());
                }
            }//Update Role
            else
            {
                var existingRole = await _roleManager.FindByIdAsync(request.Id);
                existingRole.Name = request.Name;
                existingRole.NormalizedName = request.NormalizedName!.ToUpper();
                await _roleManager.UpdateAsync(existingRole);
                return await Result<string>.SuccessAsync(string.Format("Role {0} Updated.", existingRole.Name));
            }
        }

        /// <summary>
        /// Get all permissions of role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>

        [HttpGet]
        //[Authorize(Policy = Permissions.User.View)]
        [Route("GetPermission/{roleId}")]
        public async Task<IActionResult> GetAllPermissionsAsync(string roleId)
        {
            var model = new PermissionResponse();
            var allPermissions = new List<RoleClaim>();

            allPermissions.GetPermissions(typeof(Permissions.User), roleId);

            var role = await _roleManager.FindByIdAsync(roleId);
            model.RoleId = roleId;
            var claims = await _roleManager.GetClaimsAsync(role);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }
            model.RoleClaims = allPermissions;
            var result = await Result<PermissionResponse>.SuccessAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Update permission to role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        //[Authorize(Policy = Permissions.User.Edit)]
        [Route("UpdatePermission")]
        public async Task<Result<string>> UpdatePermission(PermissionResponse model)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                var claims = await _roleManager.GetClaimsAsync(role);
                foreach (var claim in claims)
                {
                    await _roleManager.RemoveClaimAsync(role, claim);
                }
                var selectedClaims = model.RoleClaims!.Where(a => a.Selected).ToList();
                foreach (var claim in selectedClaims)
                {
                    await _roleManager.AddPermissionClaim(role, claim.Value!);
                }
                return await Result<string>.SuccessAsync("Cập nhập Permission thành công!");
            }
            catch (System.Exception ex)
            {
                return await Result<string>.FailAsync(ex.Message);
            }
            
        }
    }
}