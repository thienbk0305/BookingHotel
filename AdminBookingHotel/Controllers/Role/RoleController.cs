using AdminBookingHotel.Models.UserAndRoleViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using NToastNotify;

namespace AdminBookingHotel.Controllers.Role
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IToastNotification _toastNotification;
        public RoleController(RoleManager<IdentityRole> roleManager, IToastNotification toastNotification)
        {
            _roleManager = roleManager;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RoleViewModel> model = new List<RoleViewModel>();
            model = await _roleManager.Roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                RoleName = r.Name,
                RoleNomalizedName = r.NormalizedName
            }).ToListAsync();
            return View(model);
        }
        
        [HttpGet]
        public IActionResult AddNewRole()
        {
            return PartialView("_AddNewRole");
        }
        
        [HttpPost]
        public async Task<IActionResult> AddNewRole(RoleViewModel model)
        {

                IdentityRole role;
                role = await _roleManager.FindByIdAsync(model.RoleName);
                if (role == null)
                {
                    role = new IdentityRole();
                    role.Id = Guid.NewGuid().ToString();
                    role.Name = model.RoleName;
                    role.NormalizedName = model.RoleNomalizedName;
                    IdentityResult roleResult = await _roleManager.CreateAsync(role);
                    if (roleResult.Succeeded)
                    {
                        _toastNotification.AddSuccessToastMessage("Thêm mới Role thành công!");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại hoặc liên hệ IT");
                        return RedirectToAction("Index");
                    }
                }

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            RoleViewModel model = new RoleViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                IdentityRole role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    model.Id = role.Id;
                    model.RoleName = role.Name;
                    model.RoleNomalizedName = role.NormalizedName;
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại hoặc liên hệ IT");
                    return RedirectToAction("Index", "Role");
                }

            }
            else
            {
                _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại hoặc liên hệ IT");
                return RedirectToAction("Index", "Role");
            }
            return PartialView("_Detail", model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Detail(string id, RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role;
                role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    try
                    {
                        role.Id = model.Id;
                        role.Name = model.RoleName;
                        model.RoleNomalizedName = role.NormalizedName;
                        IdentityResult roleResult = await _roleManager.UpdateAsync(role);
                        if (roleResult.Succeeded)
                        {
                            _toastNotification.AddSuccessToastMessage("Cật nhật thông tin Role thành công!");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            _toastNotification.AddErrorToastMessage("Có lỗi xảy ra! Vui lòng kiểm tra lại hoặc liên hệ IT");
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception)
                    {
                        throw ;
                    }

                }
            }

            return RedirectToAction("Index");
        }
    }
}
