using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using static DataAccess.Models.Permissions;

namespace AdminBookingHotel.Controllers.Role
{
    public class PermissionController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IToastNotification _toastNotification;

        public PermissionController(RoleManager<IdentityRole> roleManager, IToastNotification toastNotification)
        {
            _roleManager = roleManager;
            _toastNotification = toastNotification;
        }
        public ActionResult Index(string roleId)
        {
            var listResult = new PermissionResult();
            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "Identity/Role/GetPermission/" + roleId + ""; //API Controller
            var dataJson = JsonConvert.SerializeObject(listResult);
            var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
            var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);

            if (string.IsNullOrEmpty(result))
            {
                _toastNotification.AddSuccessToastMessage("Bạn không có quyền xem danh sách Role");
                return RedirectToAction("Index");
            }

            listResult = JsonConvert.DeserializeObject<PermissionResult>(result);
            return View(listResult!.Data);

        }

        [HttpPost]
        public IActionResult Update(PermissionResponse model)
        {
            try
            {
                var listResult = new PermissionResult();
                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "Identity/Role/UpdatePermission"; //API Controller
                var dataJson = JsonConvert.SerializeObject(model);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Post, url_api, base_url, dataJson, token);

                if (string.IsNullOrEmpty(result))
                {
                    _toastNotification.AddSuccessToastMessage("Bạn không có quyền");
                    return RedirectToAction("Index", "Role");
                }

                //listResult = JsonConvert.DeserializeObject<PermissionResult>(result);

                _toastNotification.AddSuccessToastMessage("Cập Nhật Thành Công");

                return RedirectToAction("Index", new { roleId = model.RoleId });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}