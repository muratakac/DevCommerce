using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace DevCommerce.WebUI.Extensions
{
    public static class CookieExtensions
    {
        public static void AddCookie(this IResponseCookies cookie, string key, string value, DateTimeOffset? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = expireTime;
            else
                option.Expires = DateTime.Now.AddHours(1);
            cookie.Append(key,value, option);
        }

        public static void RemoveCookie(this IResponseCookies cookie, string key)
        {
            cookie.Delete(key);
        }
    }
}
