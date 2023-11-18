using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace WebApiCore.Filter
{
    public class BookingHotelAuthorizeAttribute : TypeFilterAttribute
    {
        public BookingHotelAuthorizeAttribute(string functionCode = "DEFAULT", string permission = "VIEW") :
            base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { functionCode, permission };
        }
    }
    public class AuthorizeActionFilter : IAsyncAuthorizationFilter
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
           
            throw new NotImplementedException();
        }
    }
}