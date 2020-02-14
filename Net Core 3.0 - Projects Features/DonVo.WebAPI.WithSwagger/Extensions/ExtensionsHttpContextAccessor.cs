using Microsoft.AspNetCore.Http;

namespace DonVo.WebAPI.WithSwagger.Extensions
{
    public static class ExtensionsHttpContextAccessor
    {
        public static string GetIp(this IHttpContextAccessor accessor)
        {
            return accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}