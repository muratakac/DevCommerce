using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DevCommerce.WebUI.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) :
                                  JsonConvert.DeserializeObject<T>(value);
        }
    }
}

/*
//Set
    HttpContext.Session.Set<DateTime>(SessionKeyDate, DateTime.Now);
//Get
    var date = HttpContext.Session.Get<DateTime>(SessionKeyDate);
    var sessionTime = date.TimeOfDay.ToString();
*/
