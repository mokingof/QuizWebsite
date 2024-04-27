using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EducationalQuizApp.Utilities
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value, ILogger logger = null)
        {
            try
            {
                session.SetString(key, JsonSerializer.Serialize(value));
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Error serializing data for session key {SessionKey}", key);
                throw;
            }
        }

        public static T Get<T>(this ISession session, string key, ILogger logger = null)
        {
            var value = session.GetString(key);
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            try
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            catch (JsonException ex)
            {
                logger?.LogError(ex, "Error deserializing data for session key {SessionKey}", key);
                throw;
            }
        }
    }
}
