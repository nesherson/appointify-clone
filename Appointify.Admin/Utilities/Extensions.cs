using System.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Appointify.Admin.Utilities
{
    public static class Extensions
    {
        public static T GetObject<T>(this ISession session, string key)
        {
            var json = session.GetString(key);
            if (string.IsNullOrEmpty(json))
                return default;

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static void SetObject<T>(this ISession session, string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            session.SetString(key, json);
        }

        public static bool HasKey(this ISession session, string key)
        {
            return session.Keys.Contains(key);
        }

        public static bool IsSet(this string input)
        {
            return !string.IsNullOrWhiteSpace(input) && !string.IsNullOrEmpty(input);
        }

        public static bool IsNotSet(this string input)
        {
            return !IsSet(input);
        }
    }
}
