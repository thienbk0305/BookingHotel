using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CC247.Permission
{
    internal class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; private set; }
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}
}