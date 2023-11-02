using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using NToastNotify;
using Newtonsoft.Json;
using DataAccess.Models;
using System.Collections.Generic;

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
        public IActionResult Index()
        {
            var listResult = new RoleResult();
            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "Identity/Role/Roles"; //API Controller
            var dataJson = JsonConvert.SerializeObject(listResult);
            var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
            var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);

            if (string.IsNullOrEmpty(result))
            {
                _toastNotification.AddSuccessToastMessage("Bạn không có quyền");
                return RedirectToAction("Index");
            }

            listResult = JsonConvert.DeserializeObject<RoleResult>(result);
            return View(listResult);
        }

        [HttpGet]
        public IActionResult AddNewRole()
        {
            return PartialView("_AddNewRole");
        }

        [HttpGet]
        public IActionResult Detail(string id)
        {
            var listResult = new RoleResultById();
            var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
            var base_url = "Identity/Role/GetRole/"+id+""; //API Controller
            var dataJson = JsonConvert.SerializeObject(listResult);
            var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
            var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Get, url_api, base_url, dataJson, token);

            if (string.IsNullOrEmpty(result))
            {
                _toastNotification.AddSuccessToastMessage("Bạn không có quyền");
                return RedirectToAction("Index", "Role");
            }

            listResult = JsonConvert.DeserializeObject<RoleResultById>(result);
            return PartialView("_Detail", listResult!.Data);

        }

        [HttpPost]
        public IActionResult InsertUpdateRole(RoleResponse model)
        {
            try
            {
                var listResult = new RoleResult();
                var url_api = System.Configuration.ConfigurationManager.AppSettings["URL_API"] ?? "https://localhost:7219/api/";
                var base_url = "Identity/Role/Save"; //API Controller
                var dataJson = JsonConvert.SerializeObject(model);
                var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"]!.ToString() : string.Empty;
                var result = Common.HttpHelper.WebPost_WithToken(RestSharp.Method.Post, url_api, base_url, dataJson, token);

                if (string.IsNullOrEmpty(result))
                {
                    _toastNotification.AddSuccessToastMessage("Bạn không có quyền");
                    return RedirectToAction("Index", "Role");
                }

                listResult = JsonConvert.DeserializeObject<RoleResult>(result);

                if (listResult!.Messages![0] == "Role này đã tồn tại")
                {
                    _toastNotification.AddErrorToastMessage("Role này đã tồn tại! Vui lòng kiểm tra lại");
                    return RedirectToAction("Index", "Role");
                }
                _toastNotification.AddSuccessToastMessage(listResult!.Messages!.FirstOrDefault());
                return RedirectToAction("Index", "Role");

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
