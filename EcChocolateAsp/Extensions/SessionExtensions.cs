using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcChocolateAsp.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            // kommer ni ihåg JavaScript? = ) JSON.stringify()
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            Debug.WriteLine(value);
            // kommer ni ihåg JavaScript? = ) JSON.parse()
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
