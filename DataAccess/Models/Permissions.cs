using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }
        public static class User
        {
            public const string View = "Permissions.User.View";
            public const string Create = "Permissions.User.Create";
            public const string Edit = "Permissions.User.Edit";
            public const string Delete = "Permissions.User.Delete";
        }
        public class RoleClaim
        {
            public string? Type { get; set; }
            public string? Value { get; set; }
            public bool Selected { get; set; }
        }
        public class PermissionResponse
        {
            public string? RoleId { get; set; }
            public string? RoleName { get; set; }
            public List<RoleClaim>? RoleClaims { get; set; }
        }
        public class PermissionResult
        {
            public PermissionResponse? Data { get; set; }
            public List<object>? Messages { get; set; }
            public bool Succeeded { get; set; }
        }
    }
}