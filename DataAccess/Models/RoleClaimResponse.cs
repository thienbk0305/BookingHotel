using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class RoleResponse
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }

    }
    public class RoleResult
    {
        public List<RoleResponse>? Data { get; set; }
        public List<string>? Messages { get; set; }
        public bool Succeeded { get; set; }
    }

    public class RoleResultById
    {
        public RoleResponse? Data { get; set; }
        public List<string>? Messages { get; set; }
        public bool Succeeded { get; set; }
    }
    public class ReturnData
    {
        public string? ResponseCode { get; set; }
        public string? Description { get; set; }
        public string? Extention { get; set; }
        public string? Fullname { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class TokenInfor : ReturnData
    {
        public string? token { get; set; }
        public string? expiration { get; set; }
    }


}